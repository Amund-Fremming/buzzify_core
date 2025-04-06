using Domain.Abstractions;
using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;

[ApiController]
public abstract class ReadControllerBase<T> : ControllerBase where T : class
{
    private readonly IRepository<T> _repository;

    public ReadControllerBase()
        => _repository = HttpContext.RequestServices.GetService<IRepository<T>>()
            ?? throw new Exception(nameof(IRepository<T>));

    [HttpGet]
    public async Task<ActionResult<T>> Get(int id)
        => (await _repository.GetById(id))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpGet("page")]
    public async Task<ActionResult<IEnumerable<T>>> GetPage(PagedRequest page)
        => (await _repository.GetPage(page))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}