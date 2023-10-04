using API_Host.Services;
using Database.Services;
using DTO;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class ScheduleController : ControllerBase
{
    private readonly IHashids _hashids;
    private readonly DTOConverter _converter;
    private readonly IScheduleService _scheduleService;

    public ScheduleController (IHashids hashids, DTOConverter converter, IScheduleService scheduleService)
    {
        _hashids = hashids;
        _converter = converter;
        _scheduleService = scheduleService;
    }

    [HttpPost("TEST")]
    public async Task<ActionResult> GenerateSchedule (int doctorid, DateTime start)
    {
        await _scheduleService.GenerateScheduleAsync(start, doctorid);
        return NoContent();
    }

    [HttpGet("{doctorid:hashid}")]
    public async Task<ActionResult<List<ScheduleDTO>>> GetScheduleByDoctor ([FromRoute] string doctorid,
                                                                            DateTime start,
                                                                            DateTime finish)
    {
        var rawID = _hashids.Decode(doctorid);
        if (rawID.Length == 0) {
            return NotFound();
        }

        var list = await _scheduleService.GetScheduleForDoctor(rawID[0], start, finish);

        return Ok(list.Select(_converter.ConvertSchedule));
    }

    [HttpPost("{doctorid:hashid}/{clientid:hashid}")]
    public async Task<ActionResult> AddSchedule ([FromRoute] string doctorid,
                                                 [FromRoute] string clientid,
                                                 DateTime date)
    {
        var docID = _hashids.Decode(doctorid);
        if (docID.Length == 0) {
            return NotFound();
        }

        var schedule = await _scheduleService.Repository.FirstOrDefaultAsync(s => s.ID == docID[0]
                                                                                  && s.Date == date);

        if (schedule is null) {
            return NotFound("Не найден сщедуль");
        }

        if (schedule.ClientId is not null) {
            return BadRequest("ЗАНЯТА УЙДИ");
        }

        schedule.ClientId = _hashids.Decode(clientid)[0];
        await _scheduleService.Repository.UpdateAsync(schedule);

        return Ok();
    }
}