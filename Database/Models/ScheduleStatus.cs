using Database.Flags;

namespace Database.Models;

public class ScheduleStatus : IEntityModel
{
    public int ID { get; set; }

    public string Status { get; set; } = null!;
}