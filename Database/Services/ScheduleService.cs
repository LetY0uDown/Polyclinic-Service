using Database.Repositories;
using Models;

namespace Database.Services;

public sealed class ScheduleService : IScheduleService
{
    public ScheduleService(IRepository<Schedule> repository)
    {
        Repository = repository;
    }

    public IRepository<Schedule> Repository { get; }

    public async Task GenerateScheduleAsync(DateTime start, int doctorID)
    {
        start = new DateTime(start.Year, start.Month, start.Day, 7, 0, 0);

        var finish = start.AddDays(5);

        for (var date = start; date < finish; date = date.AddHours(2)) {
            if (date.Hour > 18) {
                date = date.AddDays(1);
                date = new DateTime(date.Year, date.Month, date.Day, 7, 0, 0);
            }

            if (date.DayOfWeek == DayOfWeek.Saturday)
                break;

            await Repository.AddAsync(new Schedule {
                DoctorId = doctorID,
                Date = date,
                StatusId = 1
            });
        }
    }

    public async Task<List<Schedule>> GetScheduleForDoctor(int doctorID, DateTime start, DateTime finish)
    {
        return await Repository.WhereAsync(s => s.DoctorId == doctorID);
    }
}