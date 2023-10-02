using API_Host.Services;
using Database.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class ClientsController : ControllerBase
{
    private readonly IHashids _hashId;
    private readonly IClientService _clientService;
    private readonly DTOConverter _dtoConverter;

    public ClientsController (IHashids hashId, IClientService clientService,
                              DTOConverter dtoConverter)
    {
        _hashId = hashId;
        _clientService = clientService;
        _dtoConverter = dtoConverter;
    }

    [HttpPut("{hashid:hashid}")]
    public async Task<ActionResult> UpdateClientData ([FromRoute] string id,
                                                      [FromBody] DTO.ClientDTO clientData)
    {
        if (id != clientData.ID) {
            return BadRequest("Какую-то фигню ты прислал");
        }

        var client = await GetClientOrNull(id);

        if (client is null) {
            return NotFound("Ошибка.Данные не найдены D:");
        }

        client.Name = clientData.Name;
        client.LastName = clientData.LastName;
        client.Patronymic = clientData.Patronymic;

        await _clientService.Repository.UpdateAsync(client);

        return NoContent();
    }

    [HttpGet("{hashid:hashid}")]
    public async Task<ActionResult<DTO.ClientDTO>> GetClient ([FromRoute] string id)
    {
        var client = await GetClientOrNull(id);

        if (client is null) {
            return NotFound("Ошибка.Данные не найдены D:");
        }

        return Ok(_dtoConverter.ConvertClient(client));
    }

    /// <summary>
    /// Расшифровывает hash id и ищет клиента в базе
    /// </summary>
    /// <param name="hashid"></param>
    /// <returns>null, если hash id предоставлен неверно или клиент не был найден, иначе возвращает клиента</returns>
    private async Task<Client?> GetClientOrNull (string hashid)
    {
        var rawID = _hashId.Decode(hashid);
        if (rawID.Length == 0) {
            return null;
        }

        return await _clientService.Repository.FindAsync(rawID[0]);
    }
}