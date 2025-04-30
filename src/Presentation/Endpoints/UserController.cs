using Application.Contracts;
using Domain.DTOs;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPut("{userId:int}")]
    public async Task<IActionResult> UpdateUserActivity(int userId)
        => (await service.UpdateUserActivity(userId))
            .Resolve(
                suc => Ok(),
                err => BadRequest(err.Message));

    [HttpPost("create/guest")]
    public async Task<IActionResult> CreateGuestUser()
        => (await service.CreateGuestUser())
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpPost("create/registered")]
    public async Task<IActionResult> CreateGuestUser([FromBody] RegisterUserRequest request)
        => (await service.CreateRegisteredUser(request.Name, request.Email, request.Password))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> DoesUserExist(int userId)
        => (await service.DoesUserExist(userId))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    // TODO: register
    // TODO: login
    // TODO: refresh token
}