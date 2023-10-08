using Database.Repositories;
using Models;
using Models.IDs;

namespace Database.Services;

public interface IScheduleService
{
    IRepository<Schedule> Repository { get; }

    Task GenerateScheduleAsync (DateTime start, DoctorID doctorID);

    Task<List<Schedule>> GetScheduleForDoctor (DoctorID doctorID, DateTime start, DateTime finish);

    Task<List<Schedule>> GetFreeSchedulesBySpeciality (SpecialityID specialityID, DateTime start);

    /// Находит запись в таблице БД по её ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Найденную запись или null</returns>
    Task<Schedule?> FindAsync (ScheduleID id);
}