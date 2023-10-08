using API_Host.Services;
using Database.Services;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

/// <summary>
/// Контроллер, отвечающий за действия с докторами
/// </summary>
[ApiController, Route("[controller]/")]
public sealed class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly DTOConverter _converter;

    public DoctorsController (DTOConverter converter, IDoctorService doctorService)
    {
        _converter = converter;
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DoctorDTO>>> GetDoctors ()
    {
        var doctors = await _doctorService.Repository.GetAllAsync();

        return Ok(doctors.Select(_converter.ConvertDoctor));
    }

    [HttpGet("{id:guid}"), Authorize]
    public async Task<ActionResult<DoctorDTO>> GetDoctorByID ([FromRoute] Guid id)
    {
        var doctor = await _doctorService.FindAsync(id);

        if (doctor is null) {
            return NotFound("Доктор не найден");
        }

        return Ok(_converter.ConvertDoctor(doctor));
    }

    /// <summary>
    /// Метод для получения списка докторов с определённой специальностью
    /// </summary>
    /// <param name="specID">ID специльности</param>
    /// <returns></returns>
    [HttpGet("Speciality/{specID:guid}")]
    public async Task<ActionResult<List<Doctor>>> GetDoctorsBySpeciality ([FromRoute] Guid specID)
    {
        var doctors = await _doctorService.Repository.WhereAsync(d => d.SpecialityId == specID);

        return Ok(doctors.Select(_converter.ConvertDoctor));
    }
}