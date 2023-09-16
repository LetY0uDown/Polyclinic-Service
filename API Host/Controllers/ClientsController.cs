using API_Host.Models;
using API_Host.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public class ClientsController : ControllerBase
{
    private readonly PolyclinicContext _context;

    private readonly ILogger<ClientsController> _logger;

    public ClientsController (PolyclinicContext context, ILogger<ClientsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPut("{id:guid}"), Authorize]
    public async Task<ActionResult> UpdateClientData ([FromRoute] string id, Client clientData)
    {
        try {
            if (id != clientData.ID) {
                return BadRequest("Какую-то фигню ты прислал");
            }

            var client = await _context.Clients.FindAsync (id);

            if (client is null) {
                return NotFound("Чё-то случилось. Если бы мы знали, что это такое, но мы не знаем, что это такое");
            }

            client.Name = clientData.Name;
            client.LastName = clientData.LastName;
            client.SecondName = clientData.SecondName;

            await _context.SaveChangesAsync();

            return NoContent();
        } catch (Exception ex) {
            _logger.LogError(ex, "Error occured while trying to update client's data");
            return BadRequest("Ой. " + ex.Message);
        }
    }

    [HttpGet("{id:guid}"), Authorize]
    public async Task<ActionResult<Client>> GetClient ([FromRoute] string id)
    {
        try {
            var client = await _context.Clients.FindAsync(id);

            if (client is null) {
                return NotFound("Ошибка. Данные не найдены D:");
            }

            return Ok(client);
        }
        catch (Exception ex) {
            _logger.LogError(ex, "Error occured while trying to get client by ID");
            return BadRequest("Ой. " + ex.Message);
        }
    }
}