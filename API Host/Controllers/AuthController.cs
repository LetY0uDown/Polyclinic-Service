using API_Host.Services.Interfaces;
using Database.Services;
using DTO.Auth;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Models;

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
        var (client, error) = await _clientService.FindByDataAsync(data.EMail, data.Login);

        if (client is null) {
            return NotFound(error);
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
        var (clientExist, error) = await _clientService.CheckIfClientExistAsync(data.EMail, data.Login);

        if (clientExist) {
            return BadRequest(error);
        }

        var client = new Client {
            Login = data.Login,
            Email = data.EMail,
            Password = _hasher.Hash(data.Password)
        };

        await _clientService.Repository.AddAsync(client);

        var auth = new AuthorizationData(_hashids.Encode(client.ID));

        return Ok(auth);
    }
}