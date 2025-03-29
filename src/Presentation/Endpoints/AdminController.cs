using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserActivityData()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> SetModalNotification()
    {
        throw new NotImplementedException();
    }
}