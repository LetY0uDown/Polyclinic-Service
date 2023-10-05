using API_Host.Services;
using Database.Services;
using DTO;
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

    [HttpPost("TEST")]
    public async Task<ActionResult> GenerateSchedule (Guid doctorid, DateTime start)
    {
        await _scheduleService.GenerateScheduleAsync(start, doctorid);
        return NoContent();
    }

    [HttpGet("{doctorid:guid}")]
    public async Task<ActionResult<List<ScheduleDTO>>> GetScheduleByDoctor ([FromRoute] Guid doctorid,
                                                                            DateTime start,
                                                                            DateTime finish)
    {
        var list = await _scheduleService.GetScheduleForDoctor(doctorid, start, finish);

        return Ok(list.Select(_converter.ConvertSchedule));
    }

    [HttpPost("{doctorid:guid}/{clientid:guid}")]
    public async Task<ActionResult> AddSchedule ([FromRoute] Guid doctorid,
                                                 [FromRoute] Guid clientid,
                                                 DateTime date)
    {
        var schedule = await _scheduleService.Repository.FirstOrDefaultAsync(s => s.ID == doctorid
                                                                                  && s.Date == date);

        if (schedule is null) {
            return NotFound("Не найден сщедуль");
        }

        if (schedule.ClientId is not null) {
            return BadRequest("ЗАНЯТА УЙДИ");
        }

        schedule.ClientId = clientid;
        await _scheduleService.Repository.UpdateAsync(schedule);

        return Ok();
    }
}