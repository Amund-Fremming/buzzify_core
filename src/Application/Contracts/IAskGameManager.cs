using Domain.Entities.Ask;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IAskGameManager
{
    Task<Result<int>> CreateGame(int userId, string name, string? description = null, Category? category = null);

    Task<Result> AddQuestion(int gameId, string question);

    Task<Result<AskGame>> StartGame(int userId, int gameId);

    Task<Result<AskGame>> StartExistingGame(int userId, int gameId);
}