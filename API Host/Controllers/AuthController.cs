using API_Host.Services;
using Database.Services;
using DTO.Auth;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

/// <summary>
/// Контроллер, отвечающий за авторизацию клиента
/// </summary>
[ApiController, Route("[controller]/")]
public sealed class AuthController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly JWTTokenGenerator _jwtGenerator;

    public AuthController (IClientService clientService, JWTTokenGenerator jwtGenerator)
    {
        _clientService = clientService;
        _jwtGenerator = jwtGenerator;
    }

    /// <summary>
    /// Метод для авторизации и выдачи токена существующему клиенту
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<ActionResult<AuthorizationData>> AuthorizeClient ([FromBody] LoginData data)
    {
        // Находим клиента то его почте и логину
        var (client, error) = await _clientService.FindByDataAsync(data.EMail, data.Login);

        // Проверяем что он существует
        if (client is null) {
            return NotFound(error);
        }

        // Шифруем пароль и проверяем его на правильность
        var hashedPass = _clientService.HashPassword(data.Password);

        if (client.Password != hashedPass) {
            return Unauthorized("Неверный пароль. Попробуйте ввести его ещё раз, или восстановить доступ к вашей учётной записи.");
        }

        // Возвращаем клиенту его ID и токен для авторизации
        var auth = new AuthorizationData {
            ID = client.ID.Value,
            JWTToken = _jwtGenerator.GetToken(data.EMail, data.Password)
        };

        return Ok(auth);
    }

    /// <summary>
    /// Метод для регистрации нового клиента
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost("Register")]
    public async Task<ActionResult<AuthorizationData>> RegisterClient ([FromBody] LoginData data)
    {
        // Проверяем, нет ли уже клиента с такой почтой и логином
        var (clientExist, error) = await _clientService.CheckIfClientExistAsync(data.EMail, data.Login);

        if (clientExist) {
            return BadRequest(error);
        }

        // Создаём нового клиента, шифруем ему пароль
        var client = new Client {
            Login = data.Login,
            Email = data.EMail,
            Password = _clientService.HashPassword(data.Password)
        };

        await _clientService.Repository.AddAsync(client);
        
        // Возвращаем клиенту его ID и токен для авторизации
        var auth = new AuthorizationData {
            ID = client.ID.Value,
            JWTToken = _jwtGenerator.GetToken(data.EMail, data.Password)
        };

        return Ok(auth);
    }
}