using Domain.DTOs;
using Domain.Entities.Ask;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface IAskGameManager
{
    Task<Result<CreateGameResponse>> CreateGame(int userId, string name, string description = "", Category category = Category.Random);

    Task<Result<int>> AddQuestion(int gameId, string question);

    Task<Result<AskGame>> StartGame(int gameId);
}