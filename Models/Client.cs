using Tools.Flags;

namespace Models;

public class Client : IEntityModel, IDTOConvertable
{
    public int ID { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? LastName { get; set; }

    public List<Schedule> Schedules { get; set; } = new List<Schedule>();
}