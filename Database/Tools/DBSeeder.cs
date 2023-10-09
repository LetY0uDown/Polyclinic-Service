using Microsoft.EntityFrameworkCore;
using Models;
using Models.IDs;

namespace Database.Tools;

internal static class DBSeeder
{
    /// <summary>
    /// Заполняет БД начальными данными, если их там ещё нет
    /// </summary>
    /// <param name="modelBuilder">Ссылка на modelBuilder который, собственно, записывает данные</param>
    public static void SeedData (ModelBuilder modelBuilder)
    {
        var specialities = GetSpecialities();
        var doctors = GetDoctors(specialities);

        modelBuilder.Entity<ScheduleStatus>().HasData(GetScheduleStatuses());

        modelBuilder.Entity<Cabinet>().HasData(GetCabinets());

        modelBuilder.Entity<Speciality>().HasData(specialities);                

        modelBuilder.Entity<Doctor>().HasData(doctors);
    }

    private static Speciality[] GetSpecialities ()
    {
        return new Speciality[] {
            new Speciality() {
                ID  = SpecialityID.New(),
                Title = "Хирург"
        },  new Speciality() {
                ID  = SpecialityID.New(),
                Title = "Практолог"
        },  new Speciality() {
                ID  = SpecialityID.New(),
                Title = "Стоматолог"
        },  new Speciality() {
                ID  = SpecialityID.New(),
                Title = "Терапевт"
        }};
    }

    private static Doctor[] GetDoctors(Speciality[] specialities)
    {
        return new Doctor[] {
            new Doctor() {
                ID  = DoctorID.New(),
                Name =          "Акакий",
                LastName =      "Акакьев",
                Patronymic =    "Акакиевич",
                SpecialityId = specialities[1].ID,
                CabinetNumber = 69
        }, new Doctor() {
                ID  = DoctorID.New(),
                Name =          "Ольга",
                LastName =      "Иванова",
                Patronymic =    "Викторовна",
                SpecialityId = specialities[2].ID,
                CabinetNumber = 12
        }, new Doctor() {
                ID  = DoctorID.New(),
                Name =          "Евгений",
                LastName =      "Смирнов",
                Patronymic =    "Олегович",
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