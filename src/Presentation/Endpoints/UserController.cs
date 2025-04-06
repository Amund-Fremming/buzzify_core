using Application.Contracts;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPut("{userId:int}")]
    public async Task<IActionResult> UpdateUserActivity(int userId)
        => (await userService.UpdateUserActivity(userId))
            .Resolve(
                suc => Ok(),
                err => BadRequest(err.Message));
}