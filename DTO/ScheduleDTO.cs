namespace DTO;

public class ScheduleDTO : Tools.Flags.DTO
{
    public Guid ID { get; set; }

    public DateTime Date { get; init; }

    public string? Status { get; init; } = string.Empty;

    public DoctorDTO Doctor { get; init; } = null!;

    public ClientDTO? Client { get; init; } = null!;
}