using API_Host.Models;
using API_Host.Services;
using API_Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public class AuthController : ControllerBase
{
    private readonly IStringHasher _hasher;
    private readonly JWTTokenGenerator _jwtTokenGenerator;

    private readonly ILogger<AuthController> _logger;

    private readonly IValidator<LoginData> _dataValidator;

    private readonly PolyclinicContext _context;

    public AuthController (PolyclinicContext context, IStringHasher hasher, JWTTokenGenerator jwtTokenGenerator, ILogger<AuthController> logger, IValidator<LoginData> dataValidator)
    {
        _context = context;
        _hasher = hasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
        _dataValidator = dataValidator;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthorizationData>> AuthorizeClient ([FromBody] LoginData data)
    {
        try {
            if (!_dataValidator.IsValid(data, out var error)) {
                return BadRequest(error);
            }

            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == data.EMail);

            if (client is null) {
                return NotFound("Аккаунта с таким E-Mail адресом не существует. Проверьте правильность ввода или зарегистрируйтесь.");
            }

            var hashedPass = _hasher.Hash(data.Password);
            
            if (client.Password != hashedPass) {
                return Unauthorized("Неверный пароль. Попробуйте ввести его ещё раз, или восстановить доступ к вашей учётной записи.");
            }

            var auth = new AuthorizationData(client.ID, _jwtTokenGenerator.GetToken(client.Email, client.Password));

            return Ok(auth);

        } catch (Exception ex) {
            _logger.LogError(ex, "Error occurred while trying to authorize client");
            return BadRequest("Ой. " + ex.Message);
        }
    }

    [HttpPost("Register")]
    public async Task<ActionResult<AuthorizationData>> RegisterClient ([FromBody] LoginData data)
    {
        try {
            if (!_dataValidator.IsValid(data, out var error)) {
                return BadRequest(error);
            }

            if (await _context.Clients.AnyAsync(c => c.Email == data.EMail)) {
                return BadRequest("Данная электронная почта уже зарегистрирована в системе. Попробуйте ввести другую, или восстановить пароль к вашей учётной записи.");
            }

            var client = new Client {
                ID = Guid.NewGuid().ToString(),
                Email = data.EMail,
                Password = _hasher.Hash(data.Password)
            };

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            var auth = new AuthorizationData(client.ID, _jwtTokenGenerator.GetToken(client.Email, client.Password));

            return Ok(auth);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error occurred while trying to register new client");
            return BadRequest("Ой. " + ex.Message);
        }
    }
}