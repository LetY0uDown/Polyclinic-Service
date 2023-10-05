using Tools;
using Tools.Flags;

namespace Models;

public readonly record struct DoctorID : IStronglyTypedID<Guid>
{
    public DoctorID (Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }    

    public static implicit operator DoctorID (Guid id) => new(id);

    public static explicit operator Guid (DoctorID id) => id.Value;

    public static bool operator == (Guid guid, DoctorID id) => 
        guid == id.Value;

    public static bool operator != (Guid guid, DoctorID id) =>
        guid != id.Value;

    public static Guid New ()
    {
        return Guid.NewGuid ();
    }
}

public class Doctor : IEntityModel, IDTOConvertable
{
    public DoctorID ID { get; set; } = DoctorID.New ();

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public SpecialityID? SpecialityId { get; set; }

    public CabinetNumber CabinetNumber { get; set; }

    public Cabinet? Cabinet { get; set; }

    public List<Schedule> Schedules { get; set; } = new List<Schedule>();

    public Speciality? Speciality { get; set; }
}