using Domain.Entities.Spin;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Endpoints;

[Route("api/v1/[controller]")]
public class SpinGameController : ReadControllerBase<SpinGame>
{
    [HttpGet]
    public async Task<IActionResult> PlayerExists()
    {
        throw new NotImplementedException();
    }
}