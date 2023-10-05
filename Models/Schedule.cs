using Tools;
using Tools.Flags;

namespace Models;

public readonly record struct ScheduleID : IStronglyTypedID<Guid>
{
    public ScheduleID (Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator ScheduleID (Guid id) => new(id);

    public static explicit operator Guid (ScheduleID id) => id.Value;

    public static bool operator == (Guid guid, ScheduleID id) =>
        guid == id.Value;

    public static bool operator != (Guid guid, ScheduleID id) =>
        guid != id.Value;

    public static Guid New ()
    {
        return Guid.NewGuid ();
    }
}

public class Schedule : IEntityModel, IDTOConvertable
{
    public ScheduleID ID { get; set; } = ScheduleID.New();

    public DateTime Date { get; set; }

    public ClientID? ClientId { get; set; }

    public DoctorID? DoctorId { get; set; }

    public ScheduleStatusID StatusId { get; set; }

    public Client? Client { get; set; }

    public Doctor? Doctor { get; set; }

    public ScheduleStatus Status { get; set; } = null!;
}