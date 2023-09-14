namespace API_Host.Models;

public class Schedule
{
    public string ID { get; set; }

    public DateTime Date { get; set; }

    public string? ClientId { get; set; }

    public string? DoctorId { get; set; }

    public int StatusId { get; set; }

    public Client? Client { get; set; }

    public Doctor? Doctor { get; set; }

    public ScheduleStatus Status { get; set; } = null!;
}