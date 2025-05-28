using Application.Contracts;
using Domain.Abstractions;
using Domain.Contracts;
using Domain.DTOs;
using Domain.Entities.Ask;
using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Endpoints;

[ApiController]
[Route("api/v1/[controller]")]
public class AskGameController(IAskGameManager manager, IAskGameRepository repository, IGenericRepository genericRepository) : ControllerBase
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
    
    
    [HttpPost("page")]
    public async Task<IActionResult> GetPage([FromBody] PagedRequest page)
        => (await genericRepository.GetPage<AskGame>(page))
            .Resolve(
                suc => Ok(suc.Data),
                err => BadRequest(err.Message));
}