using Application.Contracts;
using Domain.Abstractions;
using Domain.Entities.Spin;
using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class SpinGameController(ISpinGameManager manager, IGenericRepository genericRepository) : ControllerBase
{
    [HttpPost("{userId:int}/{gameId:int}")]
    public async Task<IActionResult> InactivatePlayer(int userId, int gameId)
        => (await manager.InactivatePlayer(userId, gameId))
            .Resolve(
                suc => Ok(),
                err => BadRequest(err.Message));
    
    // INFO: the service here needs an update, it cannot be any arbitrary page, it could be a copy, or a batch of copies
    [HttpGet("page")]
    public async Task<IActionResult> GetPage([FromBody] PagedRequest pagedRequest)
        => (await genericRepository.GetPage<SpinGame>(pagedRequest))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));    
}