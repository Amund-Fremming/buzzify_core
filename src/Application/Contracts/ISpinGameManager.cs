using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface ISpinGameManager
{
    Task<Result> JoinGame(int userId, int gameId);

    Task<Result> InactivatePlayer(int userId, int gameId);

    Task<Result<int>> CreateGame(int userId, string name, Category? category = null);
}