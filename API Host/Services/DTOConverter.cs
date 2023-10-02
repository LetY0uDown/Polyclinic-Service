using Database.Models;
using HashidsNet;

namespace API_Host.Services;

/// <summary>
/// Отвечает за конвертацию объектов из БД в DTO (Data Transfer Object)
/// </summary>
public sealed class DTOConverter
{
    private readonly IHashids _hashIDs;

    public DTOConverter (IHashids hashIDs)
    {
        _hashIDs = hashIDs;
    }

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
            ID = _hashIDs.Encode(client.ID),
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
            ID = _hashIDs.Encode(doctor.ID),
            Name = doctor.Name,
            Patronymic = doctor.Patronymic,
            LastName = doctor.LastName,
            CabinetNumber = doctor.CabinetNumber ?? 0,
            SpecialityID = _hashIDs.Encode(doctor.SpecialityId ?? 0),
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
            ID = _hashIDs.Encode(schedule.ID),
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
            ID = _hashIDs.Encode(speciality.ID),
            Title = speciality.Title,
            Doctors = speciality.Doctors.Select(ConvertDoctor).ToList()
        };
    }
}