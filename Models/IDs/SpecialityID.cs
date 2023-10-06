namespace Models.IDs;

public readonly record struct SpecialityID : IStronglyTypedID<Guid>
{
    public SpecialityID(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator SpecialityID(Guid id) => new(id);

    public static explicit operator Guid(SpecialityID id) => id.Value;

    public static bool operator ==(Guid guid, SpecialityID id) =>
        guid == id.Value;

    public static bool operator !=(Guid guid, SpecialityID id) =>
        guid != id.Value;

    public static Guid New()
    {
        return Guid.NewGuid();
    }
}