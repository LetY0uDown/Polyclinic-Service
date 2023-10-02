namespace DTO;

public class ScheduleDTO : DTO
{
    public string ID { get; set; } = string.Empty;

    public DateTime Date { get; init; }

    public string? Status { get; init; } = string.Empty;

    public DoctorDTO Doctor { get; init; } = null!;

    public ClientDTO Client { get; init; } = null!;
}