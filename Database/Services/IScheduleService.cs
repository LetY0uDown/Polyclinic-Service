using Database.Repositories;
using Models;

namespace Database.Services;

public interface IScheduleService
{
    IRepository<Schedule> Repository { get; }

    Task GenerateScheduleAsync(DateTime start, DoctorID doctorID);

    Task<List<Schedule>> GetScheduleForDoctor(DoctorID doctorID, DateTime start, DateTime finish);
}