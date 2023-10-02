using API_Host.Services;
using Database.Repositories;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Host.Controllers;

[ApiController, Route("[controller]")]
public sealed class ScheduleController : ControllerBase
{
    private readonly IHashids _hashids;
    private readonly IRepository<Schedule> _repository;
    private readonly DTOConverter _converter;

    public ScheduleController (IHashids hashids, IRepository<Schedule> repository, DTOConverter converter)
    {
        _hashids = hashids;
        _repository = repository;
        _converter = converter;
    }
}