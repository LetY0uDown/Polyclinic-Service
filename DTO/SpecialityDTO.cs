namespace DTO;

public class SpecialityDTO : Tools.Flags.DTO
{
    public Guid ID { get; set; }

    public string Title { get; init; } = string.Empty;

    public List<DoctorDTO> Doctors { get; init; } = new List<DoctorDTO>();
}