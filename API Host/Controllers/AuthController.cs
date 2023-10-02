using API_Host.Services.Interfaces;
using Database.Models;
using Database.Services;
using DTO.Auth;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class AuthController : ControllerBase
{
    private readonly IStringHasher _hasher;

    private readonly IClientService _clientService;
    private readonly IHashids _hashids;

    public AuthController (IStringHasher hasher,
                           IClientService clientService, IHashids hashids)
    {
        _hasher = hasher;
        _clientService = clientService;
        _hashids = hashids;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthorizationData>> AuthorizeClient ([FromBody] LoginData data)
    {
        var client = await _clientService.FindByEmailAsync(data.EMail);

        if (client is null) {
            return NotFound("Аккаунта с таким E-Mail адресом не существует. Проверьте правильность ввода или зарегистрируйтесь.");
        }

        var hashedPass = _hasher.Hash(data.Password);

        if (client.Password != hashedPass) {
            return Unauthorized("Неверный пароль. Попробуйте ввести его ещё раз, или восстановить доступ к вашей учётной записи.");
        }

        var auth = new AuthorizationData(_hashids.Encode(client.ID));

        return Ok(auth);
    }

    [HttpPost("Register")]
    public async Task<ActionResult<AuthorizationData>> RegisterClient ([FromBody] LoginData data)
    {
        if (await _clientService.Repository.AnyAsync(c => c.Email == data.EMail)) {
            return BadRequest("Данная электронная почта уже зарегистрирована в системе. Попробуйте ввести другую, или восстановить пароль к вашей учётной записи.");
        }

        var client = new Client {
            Email = data.EMail,
            Password = _hasher.Hash(data.Password)
        };

        await _clientService.Repository.AddAsync(client);

        var auth = new AuthorizationData(_hashids.Encode(client.ID));

        return Ok(auth);
    }
}