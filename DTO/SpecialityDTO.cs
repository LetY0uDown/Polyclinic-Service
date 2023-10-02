namespace DTO;

public class SpecialityDTO : DTO
{
    public string ID { get; set; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public List<DoctorDTO> Doctors { get; init; } = new List<DoctorDTO>();
}