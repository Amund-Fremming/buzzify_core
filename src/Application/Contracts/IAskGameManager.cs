using Domain.Entities.Ask;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IAskGameManager
{
    public Task<Result<int>> CreateGame(int userId, string name, string? description = null, Category? category = null);

    public Task<Result> AddQuestion(int gameId, string question);

    public Task<Result<AskGame>> StartGame(int userId, int gameId);
}