using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("test")]
public class TestController(IBeerPriceClient client) : ControllerBase
{
    public async Task<IActionResult> Test()
        => Ok(await client.GetBeerPrices("oslo"));
}