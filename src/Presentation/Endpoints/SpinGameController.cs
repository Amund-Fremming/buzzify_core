using Application.Contracts;
using Domain.Abstractions;
using Domain.DTOs;
using Domain.Entities.Spin;
using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class SpinGameController(ISpinGameManager manager, IGenericRepository genericRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateSpinGameRequest request) =>
        (await manager.CreateGame(request.UserId, request.Name, request.Category))
        .Resolve(suc => Ok(suc.Data),
            err => BadRequest(err.Message));
    
    [HttpPost("{userId:int}/{gameId:int}")]
    public async Task<IActionResult> InactivatePlayer(int userId, int gameId)
        => (await manager.InactivatePlayer(userId, gameId))
            .Resolve(
                suc => Ok(),
                err => BadRequest(err.Message));
    
    [HttpGet("page")]
    public async Task<IActionResult> GetPage([FromBody] PagedRequest pagedRequest)
        => (await genericRepository.GetPage<SpinGame>(pagedRequest))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));    
}