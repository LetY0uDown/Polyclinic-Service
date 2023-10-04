using Tools.Flags;

namespace Models;

public class ScheduleStatus : IEntityModel
{
    public int ID { get; set; }

    public string Status { get; set; } = null!;
}