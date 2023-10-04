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
        modelBuilder.Entity<Speciality>().HasData(new Speciality() {
            ID = 1,
            Title = "Хирург"
        }, new Speciality() {
            ID = 2,
            Title = "Стоматолог"
        }, new Speciality() {
            ID = 3,
            Title = "Терапевт"
        }, new Speciality() {
            ID = 4,
            Title = "Практолог"
        });

        modelBuilder.Entity<Doctor>().HasData(new Doctor() {
            ID = 1,
            Name = "Акакий",
            LastName = "Акакьев",
            Patronymic = "Акакиевич",
            SpecialityId = 4,
            CabinetNumber = 69
        }, new Doctor() {
            ID = 2,
            Name = "Ольга",
            LastName = "Иванова",
            Patronymic = "Викторовна",
            SpecialityId = 2,
            CabinetNumber = 12
        }, new Doctor() {
            ID = 3,
            Name = "Евгений",
            LastName = "Смирнов",
            Patronymic = "Олегович",
            SpecialityId = 1,
            CabinetNumber = 2
        });

        modelBuilder.Entity<Cabinet>().HasData(new Cabinet() {
            Number = 2
        }, new Cabinet() {
            Number = 62
        }, new Cabinet() {
            Number = 12
        }, new Cabinet() {
            Number = 69
        });

        modelBuilder.Entity<ScheduleStatus>().HasData(new ScheduleStatus() {
            ID = 1,
            Status = "Ожидание приёма"
        }, new ScheduleStatus() {
            ID = 2,
            Status = "Приём оказан"
        }, new ScheduleStatus() {
            ID = 3,
            Status = "Приём не оказан"
        });
    }
}