namespace Models.IDs;

public readonly record struct ClientID : IStronglyTypedID<Guid>
{
    public ClientID(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }

    public static implicit operator ClientID(Guid id) => new(id);

    public static explicit operator Guid(ClientID id) => id.Value;

    public static bool operator ==(Guid guid, ClientID id) =>
        guid == id.Value;

    public static bool operator !=(Guid guid, ClientID id) =>
        guid != id.Value;

    public static Guid New()
    {
        return Guid.NewGuid();
    }
}