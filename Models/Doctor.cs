using Models.IDs;
using Tools.Flags;

namespace Models;

public class Doctor : IEntityModel, IDTOConvertable
{
    public DoctorID ID { get; set; } = DoctorID.New();

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public SpecialityID? SpecialityId { get; set; }

    public CabinetNumber CabinetNumber { get; set; }

    public Cabinet? Cabinet { get; set; }

    public List<Schedule> Schedules { get; set; } = new List<Schedule>();

    public Speciality? Speciality { get; set; }
}