namespace DTO;

public class DoctorDTO : DTO
{
    public string ID { get; set; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Patronymic { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public int CabinetNumber { get; init; }

    public string? SpecialityID { get; init; } = string.Empty;  

    public string? SpecialityTitle { get; init; } = string.Empty;
}