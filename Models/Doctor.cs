namespace API_Host.Models;

public class Doctor
{
    public string Id { get; set; }

    public string Name { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? SpecialityId { get; set; }

    public int? CabinetNumber { get; set; }

    public Cabinet? Cabinet { get; set; }

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public Speciality? Speciality { get; set; }
}