﻿using Application.Contracts;
using Domain.DTOs;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class SpinGameController(ISpinGameManager manager) : ControllerBase
{
    [HttpGet("user/{userId:int}/game/{gameId:int}")]
    public async Task<IActionResult> Get(int userId, int gameId)
        => (await manager.CreateGameCopy(userId, gameId))
            .Resolve(suc => Ok(suc.Data),
            err => BadRequest(err.Message));
    
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateSpinGameRequest request)
        => (await manager.CreateGame(request.UserId, request.Name, request.Category))
            .Resolve(
                suc => Ok(suc.Data), 
                err => BadRequest(err.Message));
    
    [HttpPost("user/{userId:int}/game/{gameId:int}")]
    public async Task<IActionResult> InactivatePlayer(int userId, int gameId)
        => (await manager.InactivatePlayer(userId, gameId))
            .Resolve(
                suc => Ok(),
                err => BadRequest(err.Message));
}