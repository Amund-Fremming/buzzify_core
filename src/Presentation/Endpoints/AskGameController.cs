using Application.Contracts;
using Domain.Abstractions;
using Domain.Contracts;
using Domain.DTOs;
using Domain.Entities.Ask;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Endpoints;

[Route("api/v1/[controller]")]
public class AskGameController(IAskGameManager manager, IAskGameRepository repository, IGenericRepository genericRepository) : ReadControllerBase<AskGame>(genericRepository)
{
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateAskGameRequest request)
        => (await manager.CreateGame(request.UserId, request.GameName, request.Description, request.Category))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => (await repository.GetGameWithQuestions(id))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}