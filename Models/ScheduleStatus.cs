using Models.IDs;
using Tools.Flags;

namespace Models;

public class ScheduleStatus : IEntityModel
{
    public ScheduleStatusID ID { get; set; }

    public string Status { get; set; } = null!;
}