using Application.Contracts;
using Domain.DTOs;
using Domain.Entities.Ask;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Endpoints;

[Route("api/v1/[controller]")]
public class AskGameController(IAskGameManager manager) : ReadControllerBase<AskGame>
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGame(int id)
        => (await manager.StartGame(id))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateAskGameRequest request)
        => (await manager.CreateGame(request.UserId, request.GameName, request.Description, request.Category))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}