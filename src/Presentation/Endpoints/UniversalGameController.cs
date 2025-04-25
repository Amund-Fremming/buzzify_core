using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class UniversalGameController(IUniversalGameService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPlayerToGame(int universalGameId, int userId)
        => (await service.AddPlayerToGame(universalGameId, userId))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}