using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;

[ApiController]
public abstract class GameControllerBase<T> : ControllerBase where T : GameBase
{
    private readonly IGenericRepository genericRepository;

    public GameControllerBase()
    {
        genericRepository = HttpContext.RequestServices.GetService<IGenericRepository>()
            ?? throw new RepositoryNotFoundException(nameof(IGenericRepository));
    }

    [HttpGet]
    public async Task<ActionResult<T>> Get(Guid universalId)
        => (await genericRepository.GetByUniversalId<T>(universalId))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<T>>> GetAll()
        => (await genericRepository.GetAll<T>())
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}