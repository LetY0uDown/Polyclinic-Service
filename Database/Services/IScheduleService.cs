﻿using Database.Repositories;
using Models;

namespace Database.Services;

public interface IScheduleService
{
    IRepository<Schedule> Repository { get; }

    Task GenerateScheduleAsync(DateTime start, DoctorID doctorID);

    Task<List<Schedule>> GetScheduleForDoctor(DoctorID doctorID, DateTime start, DateTime finish);

    /// Находит запись в таблице БД по её ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Найденную запись или null</returns>
    Task<Schedule?> FindAsync (ScheduleID id);
}