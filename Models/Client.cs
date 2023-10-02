namespace API_Host.Models;

public class Client
{
    public Guid ID { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public string? SecondName { get; set; }

    public string? LastName { get; set; }

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}