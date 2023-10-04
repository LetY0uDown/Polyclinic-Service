using Database.Repositories;
using Models;

namespace Database.Services;

public interface IScheduleService
{
    IRepository<Schedule> Repository { get; }

    Task GenerateScheduleAsync(DateTime start, int doctorID);

    Task<List<Schedule>> GetScheduleForDoctor(int doctorID, DateTime start, DateTime finish);
}