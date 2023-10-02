using Database.Flags;

namespace Database.Models;

public class Speciality : IEntityModel, IDTOConvertable
{
    public int ID { get; set; }

    public string Title { get; set; } = null!;

    public List<Doctor> Doctors { get; set; } = new List<Doctor>();
}