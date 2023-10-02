using API_Host.Services;
using Database.Repositories;
using DTO;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

[ApiController, Route("[controller]/")]
public class SpecialityController : ControllerBase
{
    private readonly IHashids _hashids;
    private readonly DTOConverter _converter;
    private readonly IRepository<Speciality> _repository;

    public SpecialityController (IRepository<Speciality> repository, IHashids hashids, DTOConverter converter)
    {
        _repository = repository;
        _hashids = hashids;
        _converter = converter;
    }

    [HttpGet]
    public async Task<ActionResult<List<SpecialityDTO>>> GetSpecialities ()
    {
        var specs = await _repository.GetAllAsync();

        return Ok(specs.Select(_converter.ConvertSpeciality));
    }

    [HttpGet("{id:hashid}")]
    public async Task<ActionResult<SpecialityDTO>> GetSpecialityByID ([FromRoute] string id)
    {
        var speciality = await GetSpecialityOrNull(id);

        if (speciality is null) {
            return NotFound("Специальность не найдена");
        }

        return Ok(_converter.ConvertSpeciality(speciality));
    }

    private async Task<Speciality?> GetSpecialityOrNull (string id)
    {
        var rawID = _hashids.Decode(id);
        if (rawID.Length == 0) {
            return null;
        }

        return await _repository.FindAsync(rawID[0]);
    }
}