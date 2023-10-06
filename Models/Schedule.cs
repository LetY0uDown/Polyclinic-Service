using Models.IDs;
using Tools.Flags;

namespace Models;

public class Schedule : IEntityModel, IDTOConvertable
{
    public ScheduleID ID { get; set; } = ScheduleID.New();

    public DateTime Date { get; set; }

    public ClientID? ClientId { get; set; }

    public DoctorID? DoctorId { get; set; }

    public ScheduleStatusID StatusId { get; set; }

    public Client? Client { get; set; }

    public Doctor? Doctor { get; set; }

    public ScheduleStatus Status { get; set; } = null!;
}