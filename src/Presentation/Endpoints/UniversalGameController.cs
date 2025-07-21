using Application.Contracts;
using Domain.Abstractions;
using Domain.Entities.Ask;
using Domain.Entities.Spin;
using Domain.Shared;
using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class UniversalGameController(IUniversalGameService service, IGenericRepository genericRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPlayerToGame([FromQuery] int userId, [FromQuery] int universalGameId)
        => (await service.AddPlayerToGame(userId, universalGameId))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpGet("page")]
    public async Task<ActionResult<GameBase>> GetPage([FromQuery] GameType gameType, [FromBody] PagedRequest pagedRequest)
    {
        var result = gameType switch
        {
            GameType.AskGame => await genericRepository.GetPage<AskGame>(pagedRequest),
            GameType.SpinGame => await genericRepository.GetPage<SpinGame>(pagedRequest),
            _ => new Error("Game does not exist")
        };

        return result.Resolve(
            suc => Ok(suc.Data),
            err => BadRequest(err.Message));
    }
}