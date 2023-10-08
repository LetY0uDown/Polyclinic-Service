using API_Host.Services;
using Database.Services;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Host.Controllers;

/// <summary>
/// Контроллер для действий с расписанием
/// </summary>
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

    /// <summary>
    /// Генерирует расписание до конца недели для определённого врача. Хрень какая-то, но пусть пока будет
    /// </summary>
    /// <param name="doctorid"></param>
    /// <param name="start">Дата отсчёта. Если указанный день недели - суббота, то не генирирует расписание вообще</param>
    /// <returns></returns>
    [HttpPost("Generate"), Authorize]
    public async Task<ActionResult> GenerateSchedule (Guid doctorid, DateTime start)
    {
        await _scheduleService.GenerateScheduleAsync(start, doctorid);
        return NoContent();
    }

    /// <summary>
    /// Метод для получения расписания для конкретного клиента
    /// </summary>
    /// <param name="clientid"></param>
    /// <returns></returns>
    [HttpGet("c/{clientid:guid}"), Authorize]
    public async Task<ActionResult<List<ScheduleDTO>>> GetScheduleByClient([FromRoute] Guid clientid)
    {
        var list = await _scheduleService.Repository.WhereAsync(s => s.ClientId == clientid);

        return Ok(list.Select(_converter.ConvertSchedule).OrderBy(s => s.Date));
    }

    /// <summary>
    /// Метод для получения расписания для конкретного доктора в заданном отрезке времени
    /// </summary>
    /// <param name="doctorid"></param>
    /// <param name="start">Начало временного отрезка</param>
    /// <param name="finish">Конец временного отрезка</param>
    /// <returns></returns>
    [HttpGet("d/{doctorid:guid}"), Authorize]
    public async Task<ActionResult<List<ScheduleDTO>>> GetScheduleByDoctor ([FromRoute] Guid doctorid,
                                                                            [FromQuery] DateTime start,
                                                                            [FromQuery] DateTime finish)
    {
        if (start < finish) {
            return BadRequest("Некорректные даты. Дата начала отсчёта не может быть меньше даты конца отсчёта");
        }

        var list = await _scheduleService.GetScheduleForDoctor(doctorid, start, finish);

        return Ok(list.Select(_converter.ConvertSchedule).OrderBy(s => s.Date));
    }

    /// <summary>
    /// Метод для записи клиента на определённое расписание
    /// </summary>
    /// <param name="scheduleDTO">Запись в расписании</param>
    /// <param name="clientID"></param>
    /// <returns></returns>
    [HttpPost("{clientID:guid}"), Authorize]
    public async Task<ActionResult> AddSchedule ([FromBody]ScheduleDTO scheduleDTO,
                                                 [FromRoute] Guid clientID)
    {
        var schedule = await _scheduleService.FindAsync(scheduleDTO.ID);

        // Проверяем, что такая запись существует в БД и что туда не записан никто другой. Тут потенциально может возникнуть ошибка, но этот сервис всё равно никто не будет использовать так что по-фи-гу :D
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

    /// <summary>
    /// Метод для получания записей в расписании по определённой специальности, в которые никто не записан
    /// </summary>
    /// <param name="specialityID"></param>
    /// <param name="date">Самая ранняя дата возможной записи</param>
    /// <returns></returns>
    [HttpGet("free/{specialityID:guid}")]
    public async Task<ActionResult<List<ScheduleDTO>>> GetFreeSchedulesBySpeciality ([FromRoute] Guid specialityID,
                                                                                     [FromQuery] DateTime date)
    {
        var list = await _scheduleService.GetFreeSchedulesBySpeciality(specialityID, date);
        
        return Ok(list.Select(_converter.ConvertSchedule).OrderBy(s => s.Date));
    }
}