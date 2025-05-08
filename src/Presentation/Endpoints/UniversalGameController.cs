using Application.Contracts;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class UniversalGameController(IUniversalGameService service) : ControllerBase
{
    [HttpPost("{userId:number}/{universalGameId:number}")]
    public async Task<IActionResult> AddPlayerToGame(int userId, int universalGameId)
        => (await service.AddPlayerToGame(userId, universalGameId))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}