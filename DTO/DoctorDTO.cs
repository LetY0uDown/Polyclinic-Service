namespace DTO;

public class DoctorDTO : Tools.Flags.DTO
{
    public Guid ID { get; set; }

    public string Name { get; init; } = string.Empty;

    public string Patronymic { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public int CabinetNumber { get; init; }

    public Guid? SpecialityID { get; init; } = default;

    public string? SpecialityTitle { get; init; } = string.Empty;

    public string FullName => $"{Name[0]}. {Patronymic[0]}. {LastName}";
}