using API_Host.Services;
using Database.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public class SpecialityController : ControllerBase
{
    private readonly DTOConverter _converter;
    private readonly ISpecialityService _specialityService;

    public SpecialityController (DTOConverter converter, ISpecialityService specialityService)
    {
        _converter = converter;
        _specialityService = specialityService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SpecialityDTO>>> GetSpecialities ()
    {
        var specs = await _specialityService.Repository.GetAllAsync();

        return Ok(specs.Select(_converter.ConvertSpeciality));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SpecialityDTO>> GetSpecialityByID ([FromRoute] Guid id)
    {
        var speciality = await _specialityService.FindAsync(id);

        if (speciality is null) {
            return NotFound("Специальность не найдена");
        }

        return Ok(_converter.ConvertSpeciality(speciality));
    }
}