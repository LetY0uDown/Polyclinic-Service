using API_Host.Services;
using Database.Services;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly DTOConverter _dtoConverter;

    public ClientsController (IClientService clientService,
                              DTOConverter dtoConverter)
    {
        _clientService = clientService;
        _dtoConverter = dtoConverter;
    }

    /// <summary>
    /// Метод для обновления данных в кабинете пользователя
    /// </summary>
    /// <param name="id">ID пользователя</param>
    /// <param name="clientData">Данные для обновления</param>
    /// <returns>Обновлённые данные</returns>
    [HttpPut("{id:guid}"), Authorize]
    public async Task<ActionResult<ClientDTO>> UpdateClientData ([FromRoute] Guid id,
                                                                 [FromBody] ClientDTO clientData)
    {
        // Сравниваем на всякий случай
        if (id != clientData.ID) {
            return BadRequest("Какую-то фигню ты прислал");
        }

        // Ищем клиента в БД
        var client = await _clientService.FindAsync(id);

        if (client is null) {
            return NotFound("Ошибка.Данные не найдены D:");
        }

        // Обновляем данные и сохраняем
        client.Name = clientData.Name;
        client.LastName = clientData.LastName;
        client.Patronymic = clientData.Patronymic;

        client.Login = clientData.Login;

        await _clientService.Repository.UpdateAsync(client);

        return Ok(_dtoConverter.ConvertClient(client));
    }

    /// <summary>
    /// Метод для получения данных клиента по его ID
    /// </summary>
    /// <param name="id">ID пользователя</param>
    /// <returns></returns>
    [HttpGet("{id:guid}"), Authorize]
    public async Task<ActionResult<ClientDTO>> GetClient ([FromRoute] Guid id)
    {
        var client = await _clientService.FindAsync(id);

        if (client is null) {
            return NotFound("Ошибка.Данные не найдены D:");
        }

        return Ok(_dtoConverter.ConvertClient(client));
    }
}