using Application.Contracts;
using Domain.Abstractions;
using Domain.Entities.Spin;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Endpoints;

[Route("api/v1/[controller]")]
public class SpinGameController(ISpinGameManager manager, IGenericRepository genericRepository) : ReadControllerBase<SpinGame>(genericRepository)
{
    [HttpPost("{userId:int}/{gameId:int}")]
    public async Task<IActionResult> InactivatePlayer(int userId, int gameId)
        => (await manager.InactivatePlayer(userId, gameId))
            .Resolve(
                suc => Ok(),
                err => BadRequest(err.Message));
}