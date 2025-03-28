using Domain.Entities.Spin;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Endpoints;

[Route("api/v1/[controller]")]
public class SpinGameController : ReadControllerBase<SpinGame>
{
    [HttpPut]
    public async Task<IActionResult> UpdatePlayerActivity()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> PlayerExists()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> InActivePlayer([FromQuery] int playerId, int gameId)
    {
        throw new NotImplementedException();
    }
}