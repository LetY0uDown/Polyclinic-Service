namespace API_Host.Models;

public class Speciality
{
    public int ID { get; set; }

    public string Title { get; set; } = null!;

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}