using API_Host.Services;
using Database.Repositories;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public class SpecialityController : ControllerBase
{
    private readonly DTOConverter _converter;
    private readonly IRepository<Speciality> _repository;

    public SpecialityController (IRepository<Speciality> repository, DTOConverter converter)
    {
        _repository = repository;
        _converter = converter;
    }

    [HttpGet]
    public async Task<ActionResult<List<SpecialityDTO>>> GetSpecialities ()
    {
        var specs = await _repository.GetAllAsync();

        return Ok(specs.Select(_converter.ConvertSpeciality));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SpecialityDTO>> GetSpecialityByID ([FromRoute] Guid id)
    {
        var speciality = await _repository.FindAsync(id);

        if (speciality is null) {
            return NotFound("Специальность не найдена");
        }

        return Ok(_converter.ConvertSpeciality(speciality));
    }
}