using API_Host.Services;
using Database.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public sealed class DoctorsController : ControllerBase
{
    private readonly IRepository<Doctor> _repository;
    private readonly DTOConverter _converter;

    public DoctorsController (IRepository<Doctor> repository, DTOConverter converter)
    {
        _repository = repository;
        _converter = converter;
    }

    [HttpGet]
    public async Task<ActionResult<List<DoctorDTO>>> GetDoctors ()
    {
        var doctors = await _repository.GetAllAsync();

        return Ok(doctors.Select(_converter.ConvertDoctor));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DoctorDTO>> GetDoctorByID ([FromRoute] Guid id)
    {
        var doctor = await _repository.FindAsync(id);

        if (doctor is null) {
            return NotFound("Доктор не найден");
        }

        return Ok(_converter.ConvertDoctor(doctor));
    }

    [HttpGet("Speciality/{specID:guid}")]
    public async Task<ActionResult<List<Doctor>>> GetDoctorsBySpeciality ([FromRoute] Guid specID)
    {
        var doctors = await _repository.WhereAsync(d => d.SpecialityId == specID);

        return Ok(doctors.Select(_converter.ConvertDoctor));
    }
}