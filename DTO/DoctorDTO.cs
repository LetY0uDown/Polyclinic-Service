using Models.IDs;

namespace DTO;

public class DoctorDTO : Tools.Flags.DTO
{
    public DoctorID ID { get; set; }

    public string Name { get; init; } = string.Empty;

    public string Patronymic { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public CabinetNumber CabinetNumber { get; init; }

    public SpecialityID? SpecialityID { get; init; } = default;

    public string? SpecialityTitle { get; init; } = string.Empty;
}