namespace Models.IDs;

public readonly record struct ScheduleID : IStronglyTypedID<Guid>
{
    public ScheduleID(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator ScheduleID(Guid id) => new(id);

    public static explicit operator Guid(ScheduleID id) => id.Value;

    public static bool operator ==(Guid guid, ScheduleID id) =>
        guid == id.Value;

    public static bool operator !=(Guid guid, ScheduleID id) =>
        guid != id.Value;

    public static Guid New()
    {
        return Guid.NewGuid();
    }
}