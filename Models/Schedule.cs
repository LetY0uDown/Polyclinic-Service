namespace API_Host.Models;

public class Schedule
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public Guid? ClientId { get; set; }

    public Guid? DoctorId { get; set; }

    public int StatusId { get; set; }

    public Client? Client { get; set; }

    public Doctor? Doctor { get; set; }

    public ScheduleStatus Status { get; set; } = null!;
}