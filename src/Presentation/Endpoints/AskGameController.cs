using Application.Contracts;
using Domain.Abstractions;
using Domain.DTOs;
using Domain.Entities.Ask;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Endpoints;

[Route("api/v1/[controller]")]
public class AskGameController(IAskGameManager manager, IGenericRepository genericRepository) : ReadControllerBase<AskGame>(genericRepository)
{
    [HttpPost("{gameId:int}")]
    public async Task<IActionResult> AddQuestion(int gameId, [FromQuery] string question)
        => (await manager.AddQuestion(gameId, question))
            .Resolve(
                suc => Ok(),
                err => BadRequest(err.Message));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> StartGame(int id)
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