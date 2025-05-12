using Domain.Abstractions;
using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;

[ApiController]
public abstract class ReadControllerBase<T>(IGenericRepository repository) : ControllerBase where T : class
{
    [HttpGet]
    public async Task<ActionResult<T>> Get(int id)
        => (await repository.GetById<T>(id))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpGet("page")]
    public async Task<ActionResult<IEnumerable<T>>> GetPage([FromBody] PagedRequest page)
        => (await repository.GetPage<T>(page))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}