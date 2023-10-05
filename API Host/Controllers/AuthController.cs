using Database.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using Tools.Auth;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class AuthController : ControllerBase
{
    private readonly IClientService _clientService;

    public AuthController (IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthorizationData>> AuthorizeClient ([FromBody] LoginData data)
    {
        var (client, error) = await _clientService.FindByDataAsync(data.EMail, data.Login);

        if (client is null) {
            return NotFound(error);
        }

        var hashedPass = _clientService.HashPassword(data.Password);

        if (client.Password != hashedPass) {
            return Unauthorized("Неверный пароль. Попробуйте ввести его ещё раз, или восстановить доступ к вашей учётной записи.");
        }

        var auth = new AuthorizationData(client.ID.ToString());

        return Ok(auth);
    }

    [HttpPost("Register")]
    public async Task<ActionResult<AuthorizationData>> RegisterClient ([FromBody] LoginData data)
    {
        var (clientExist, error) = await _clientService.CheckIfClientExistAsync(data.EMail, data.Login);

        if (clientExist) {
            return BadRequest(error);
        }

        var client = new Client {
            Login = data.Login,
            Email = data.EMail,
            Password = _clientService.HashPassword(data.Password)
        };

        await _clientService.Repository.AddAsync(client);

        var auth = new AuthorizationData(client.ID.ToString());

        return Ok(auth);
    }
}