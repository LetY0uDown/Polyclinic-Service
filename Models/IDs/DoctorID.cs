namespace Models.IDs;

public readonly record struct DoctorID : IStronglyTypedID<Guid>
{
    public DoctorID(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator DoctorID(Guid id) => new(id);

    public static explicit operator Guid(DoctorID id) => id.Value;

    public static bool operator ==(Guid guid, DoctorID id) =>
        guid == id.Value;

    public static bool operator !=(Guid guid, DoctorID id) =>
        guid != id.Value;

    public static Guid New()
    {
        return Guid.NewGuid();
    }
}
