using API_Host.Services;
using Database.Repositories;
using DTO;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class DoctorsController : ControllerBase
{
    private readonly IHashids _hashids;
    private readonly IRepository<Doctor> _repository;
    private readonly DTOConverter _converter;

    public DoctorsController (IHashids hashids, IRepository<Doctor> repository, DTOConverter converter)
    {
        _hashids = hashids;
        _repository = repository;
        _converter = converter;
    }

    [HttpGet]
    public async Task<ActionResult<List<DoctorDTO>>> GetDoctors ()
    {
        var doctors = await _repository.GetAllAsync();

        return Ok(doctors.Select(_converter.ConvertDoctor));
    }

    [HttpGet("{id:hashid}")]
    public async Task<ActionResult<DoctorDTO>> GetDoctorByID ([FromRoute] string id)
    {
        var doctor = await GetDoctorOrNull(id);

        if (doctor is null) {
            return NotFound("Доктор не найден");
        }

        return Ok(_converter.ConvertDoctor(doctor));
    }

    [HttpGet("Speciality/{id:hashid}")]
    public async Task<ActionResult<List<Doctor>>> GetDoctorsBySpeciality ([FromRoute] string id)
    {
        var rawID = _hashids.Decode(id);
        if (rawID.Length == 0) {
            return NotFound();
        }

        var doctors = await _repository.WhereAsync(d => d.SpecialityId == rawID[0]);

        return Ok(doctors.Select(_converter.ConvertDoctor));
    }

    private async Task<Doctor?> GetDoctorOrNull (string id)
    {
        var rawID = _hashids.Decode(id);
        if (rawID.Length == 0) {
            return null;
        }

        return await _repository.FindAsync(rawID[0]);
    }
}