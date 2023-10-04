using Tools.Flags;

namespace Models;

public class Schedule : IEntityModel, IDTOConvertable
{
    public int ID { get; set; }

    public DateTime Date { get; set; }

    public int? ClientId { get; set; }

    public int? DoctorId { get; set; }

    public int StatusId { get; set; }

    public Client? Client { get; set; }

    public Doctor? Doctor { get; set; }

    public ScheduleStatus Status { get; set; } = null!;
}