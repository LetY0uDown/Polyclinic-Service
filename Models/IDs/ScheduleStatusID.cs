namespace Models.IDs;

public readonly record struct ScheduleStatusID : IStronglyTypedID<int>
{
    public ScheduleStatusID(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static implicit operator ScheduleStatusID(int id) => new(id);

    public static explicit operator int(ScheduleStatusID id) => id.Value;

    public static bool operator ==(int num, ScheduleStatusID id) =>
        num == id.Value;

    public static bool operator !=(int num, ScheduleStatusID id) =>
        num != id.Value;

    public static int New()
    {
        return default;
    }
}