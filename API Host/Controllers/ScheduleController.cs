using API_Host.Services;
using Database.Repositories;
using Database.Services;
using DTO;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class ScheduleController : ControllerBase
{
    private readonly IHashids _hashids;
    private readonly DTOConverter _converter;
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IHashids hashids, DTOConverter converter, IScheduleService scheduleService)
    {
        _hashids = hashids;
        _converter = converter;
        _scheduleService = scheduleService;
    }

    [HttpPost("TEST")]
    public async Task<ActionResult> GenerateSchedule(int doctorid, DateTime start)
    {
        await _scheduleService.GenerateScheduleAsync(start, doctorid);
        return NoContent();
    }

    [HttpGet("{doctorid:hashid}")]
    public async Task<ActionResult<List<ScheduleDTO>>> GetScheduleByDoctor([FromRoute]string doctorid,
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
    public async Task<ActionResult> AddSchedule([FromRoute])
    {
        var schedule = await GetScheduleOrNull(scheduleDTO.ID);

        if (schedule is null) {
            return NotFound("Не найден сщедуль");
        }

        if (schedule.ClientId is not null) {
            return BadRequest("ЗАНЯТА УЙДИ");
        }

        schedule.ClientId = _hashids.Decode(scheduleDTO.Client.ID)[0];
        await _scheduleService.Repository.UpdateAsync(schedule);

        return Ok();
    }

    private async Task<Schedule?> GetScheduleOrNull(string id)
    {
        var rawID = _hashids.Decode(id);
        if (rawID.Length == 0) {
            return null;
        }

        return await _scheduleService.Repository.FindAsync(rawID[0]);
    }
}

/*
 * [HttpPost("GenerateSchedule")]
public async Task<ActionResult> GenerateSchedule(DateTime start, int idDoctor)
{
    try
    {
        start = new DateTime(start.Year, start.Month, start.Day, 7,0,0);

        var finish = start.AddDays(5);
        for (var date = start; date < finish; date = date.AddHours(2))
        {
            if (date.Hour > 18)
            {
                date = date.AddDays(1);
                date = new DateTime(date.Year, date.Month, date.Day, 7, 0, 0);
            }
            if (date.DayOfWeek == DayOfWeek.Saturday)
                break;
            await db.Schedules.AddAsync(new Schedule { IdDoctor = idDoctor, StartTime = date });
        }
        await db.SaveChangesAsync();
        return Ok();
    }
    catch  (Exception ex)
    {
        return BadRequest($"{ex.Message}");
    }
}
 * */