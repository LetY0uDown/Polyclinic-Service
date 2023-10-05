using API_Host.Services;
using Database.Services;
using DTO;
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

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientDTO>> UpdateClientData ([FromRoute] Guid id,
                                                                 [FromBody] ClientDTO clientData)
    {
        if (id != clientData.ID) {
            return BadRequest("Какую-то фигню ты прислал");
        }

        var client = await _clientService.Repository.FindAsync(id);

        if (client is null) {
            return NotFound("Ошибка.Данные не найдены D:");
        }

        client.Name = clientData.Name;
        client.LastName = clientData.LastName;
        client.Patronymic = clientData.Patronymic;

        await _clientService.Repository.UpdateAsync(client);

        return Ok(_dtoConverter.ConvertClient(client));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDTO>> GetClient ([FromRoute] Guid id)
    {
        var client = await _clientService.Repository.FindAsync(id);

        if (client is null) {
            return NotFound("Ошибка.Данные не найдены D:");
        }

        return Ok(_dtoConverter.ConvertClient(client));
    }
}