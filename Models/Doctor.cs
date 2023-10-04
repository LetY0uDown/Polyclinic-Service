using Tools.Flags;

namespace Models;

public class Doctor : IEntityModel, IDTOConvertable
{
    public int ID { get; set; }

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? SpecialityId { get; set; }

    public int? CabinetNumber { get; set; }

    public Cabinet? Cabinet { get; set; }

    public List<Schedule> Schedules { get; set; } = new List<Schedule>();

    public Speciality? Speciality { get; set; }
}