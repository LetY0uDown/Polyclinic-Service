using Microsoft.EntityFrameworkCore;
using Models;

namespace Database.Tools;

internal static class DBSeeder
{
    /// <summary>
    /// Заполняет БД начальными данными, если их там ещё нет
    /// </summary>
    /// <param name="modelBuilder">Ссылка на modelBuilder который, собственно, записывает данные</param>
    internal static void SeedData (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Speciality>().HasData(GetSpecialities());

        modelBuilder.Entity<Cabinet>().HasData(GetCabinets());

        modelBuilder.Entity<ScheduleStatus>().HasData(GetScheduleStatuses());

        modelBuilder.Entity<Doctor>().HasData(GetDoctors());
    }

    private static Speciality[] GetSpecialities ()
    {
        return new Speciality[] {
            new Speciality() {
            ID  = Guid.NewGuid(),
            Title = "Хирург"
        }, new Speciality() {
            ID  = Guid.NewGuid(),
            Title = "Стоматолог"
        }, new Speciality() {
            ID  = Guid.NewGuid(),
            Title = "Терапевт"
        }, new Speciality() {
            ID  = Guid.NewGuid(),
            Title = "Практолог"
        }};
    }

    private static Doctor[] GetDoctors()
    {
        var specialities = GetSpecialities();
        
        return new Doctor[] {
            new Doctor() {
            ID  = Guid.NewGuid(),
            Name = "Акакий",
            LastName = "Акакьев",
            Patronymic = "Акакиевич",
            SpecialityId = specialities[3].ID,
            CabinetNumber = 69
        }, new Doctor() {
            ID  = Guid.NewGuid(),
            Name = "Ольга",
            LastName = "Иванова",
            Patronymic = "Викторовна",
            SpecialityId = specialities[1].ID,
            CabinetNumber = 12
        }, new Doctor() {
            ID  = Guid.NewGuid(),
            Name = "Евгений",
            LastName = "Смирнов",
            Patronymic = "Олегович",
            SpecialityId = specialities[0].ID,
            CabinetNumber = 2
        }};
    }

    private static ScheduleStatus[] GetScheduleStatuses ()
    {
        return new ScheduleStatus[] {
            new ScheduleStatus() {
            ID = 1,
            Status = "Ожидание приёма"
        }, new ScheduleStatus() {
            ID = 2,
            Status = "Приём оказан"
        }, new ScheduleStatus() {
            ID = 3,
            Status = "Приём не оказан"
        }};
    }

    private static Cabinet[] GetCabinets ()
    {
        return new Cabinet[] {
            new Cabinet() {
            Number = 2
        }, new Cabinet() {
            Number = 62
        }, new Cabinet() {
            Number = 12
        }, new Cabinet() {
            Number = 69
        }};
    }
}