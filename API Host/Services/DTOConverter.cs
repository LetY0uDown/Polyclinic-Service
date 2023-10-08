using Models;

namespace API_Host.Services;

/// <summary>
/// Отвечает за конвертацию объектов из БД в DTO (Data Transfer Object)
/// </summary>
public sealed class DTOConverter
{
    /// <summary>
    /// Конвертирует объект из БД в DTO для отправки клиенту с сервера
    /// </summary>
    /// <param name="client">Объект для конвертации</param>
    /// <returns>Возможен null. Подумать</returns>
    public DTO.ClientDTO ConvertClient (Client client)
    {
        if (client is null)
            return null!;

        return new() {
            ID = client.ID.Value,
            Login = client.Login,
            Email = client.Email,
            Name = client.Name,
            Patronymic = client.Patronymic,
            LastName = client.LastName
        };
    }

    /// <summary>
    /// Конвертирует объект из БД в DTO для отправки клиенту с сервера
    /// </summary>
    /// <param name="doctor">Объект для конвертации</param>
    /// <returns>
    /// Может вернуть null. Подумать
    /// </returns>
    public DTO.DoctorDTO ConvertDoctor (Doctor doctor)
    {
        if (doctor is null)
            return null!;

        return new() {
            ID = doctor.ID.Value,
            Name = doctor.Name,
            Patronymic = doctor.Patronymic,
            LastName = doctor.LastName,
            CabinetNumber = doctor.CabinetNumber.Value,
            SpecialityID = doctor.SpecialityId!.Value.Value,
            SpecialityTitle = doctor.Speciality?.Title
        };
    }

    /// <summary>
    /// Конвертирует объект из БД в DTO для отправки клиенту с сервера
    /// </summary>
    /// <param name="schedule">Объект для конвертации</param>
    /// <returns>Null. Подумать</returns>
    public DTO.ScheduleDTO ConvertSchedule (Schedule schedule)
    {
        if (schedule is null)
            return null!;

        return new() {
            ID = schedule.ID.Value,
            Date = schedule.Date,
            Status = schedule.Status?.Status,
            Doctor = ConvertDoctor(schedule.Doctor!),
            Client = ConvertClient(schedule.Client!)
        };
    }

    /// <summary>
    /// Конвертирует объект из БД в DTO для отправки клиенту с сервера
    /// </summary>
    /// <param name="speciality">Объект для конвертации</param>
    /// <returns>null. Возможно.</returns>
    public DTO.SpecialityDTO ConvertSpeciality (Speciality speciality)
    {
        if (speciality is null)
            return null!;

        return new() {
            ID = speciality.ID.Value,
            Title = speciality.Title,
            Doctors = speciality.Doctors.Select(ConvertDoctor).ToList()
        };
    }
}