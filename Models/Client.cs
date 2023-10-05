using Tools;
using Tools.Flags;

namespace Models;

public readonly record struct ClientID : IStronglyTypedID<Guid>
{
    public ClientID(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }

    public static implicit operator ClientID (Guid id) => new(id);

    public static explicit operator Guid (ClientID id) => id.Value;

    public static bool operator == (Guid guid, ClientID id) =>
        guid == id.Value;

    public static bool operator != (Guid guid, ClientID id) =>
        guid != id.Value;

    public static Guid New ()
    {
        return Guid.NewGuid ();
    }
}

public class Client : IEntityModel, IDTOConvertable
{
    public ClientID ID { get; set; } = ClientID.New ();

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? LastName { get; set; }

    public List<Schedule> Schedules { get; set; } = new List<Schedule>();
}