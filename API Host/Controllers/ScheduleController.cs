using API_Host.Services;
using Database.Services;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class ScheduleController : ControllerBase
{
    private readonly DTOConverter _converter;
    private readonly IScheduleService _scheduleService;

    public ScheduleController (DTOConverter converter,
                               IScheduleService scheduleService)
    {
        _converter = converter;
        _scheduleService = scheduleService;
    }

    [HttpPost("Generate"), Authorize]
    public async Task<ActionResult> GenerateSchedule (Guid doctorid, DateTime start)
    {
        await _scheduleService.GenerateScheduleAsync(start, doctorid);
        return NoContent();
    }

    [HttpGet("c/{clientid:guid}"), Authorize]
    public async Task<ActionResult<List<ScheduleDTO>>> GetScheduleByClient([FromRoute] Guid clientid)
    {
        var list = await _scheduleService.Repository.WhereAsync(s => s.ClientId == clientid);

        return Ok(list.Select(_converter.ConvertSchedule).OrderBy(s => s.Date));
    }

    [HttpGet("d/{doctorid:guid}"), Authorize]
    public async Task<ActionResult<List<ScheduleDTO>>> GetScheduleByDoctor ([FromRoute] Guid doctorid,
                                                                            DateTime start, DateTime finish)
    {
        var list = await _scheduleService.GetScheduleForDoctor(doctorid, start, finish);

        return Ok(list.Select(_converter.ConvertSchedule).OrderBy(s => s.Date));
    }

    [HttpPost("{clientID:guid}"), Authorize]
    public async Task<ActionResult> AddSchedule ([FromBody]ScheduleDTO scheduleDTO,
                                                 [FromRoute] Guid clientID)
    {
        var schedule = await _scheduleService.FindAsync(scheduleDTO.ID);

        if (schedule is null) {
            return NotFound("Не найден сщедуль");
        }

        if (schedule.ClientId is not null) {
            return BadRequest("ЗАНЯТА УЙДИ");
        }

        schedule.ClientId = clientID;
        await _scheduleService.Repository.UpdateAsync(schedule);

        return NoContent();
    }

    [HttpGet("free/{specialityID:guid}")]
    public async Task<ActionResult<List<ScheduleDTO>>> GetFreeSchedulesBySpeciality ([FromRoute] Guid specialityID,
                                                                                     [FromQuery] DateTime date)
    {
        var list = await _scheduleService.GetFreeSchedulesBySpeciality(specialityID, date);
        
        return Ok(list.Select(_converter.ConvertSchedule).OrderBy(s => s.Date));
    }
}