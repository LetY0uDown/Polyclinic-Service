using Models.IDs;
using Tools.Flags;

namespace Models;

public class Speciality : IEntityModel, IDTOConvertable
{
    public SpecialityID ID { get; set; } = SpecialityID.New();

    public string Title { get; set; } = null!;

    public List<Doctor> Doctors { get; set; } = new List<Doctor>();
}