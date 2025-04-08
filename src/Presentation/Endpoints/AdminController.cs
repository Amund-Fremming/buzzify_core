using Application.Contracts;
using Domain.DTOs;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class AdminController(IAdminService service) : ControllerBase
{
    [HttpGet("activity/recent")]
    public async Task<IActionResult> GetRecentActivityData([FromBody] int passCode)
        => (await service.GetRecentUserActivity(passCode))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpGet("activity/historic")]
    public async Task<IActionResult> GetHistoricActivityData([FromBody] int passCode)
        => (await service.GetHistoricUserActivity(passCode))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpPost("notification/set/{id:int}")]
    public async Task<IActionResult> SetNotification(int id, [FromQuery] bool display, [FromBody] int passCode)
        => (await service.SetNotification(id, passCode, display))
            .Resolve(
                suc => Ok(),
                err => BadRequest(err.Message));

    [HttpPost("notification/create")]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationRequest request)
          => (await service.CreateNotification(request.PassCode, request.Heading, request.Message, request.Color))
              .Resolve(
                  suc => Ok(),
                  err => BadRequest(err.Message));

    public async Task<IActionResult> GetAllNotifications([FromBody] int passCode)
        => (await service.GetAll(passCode))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}