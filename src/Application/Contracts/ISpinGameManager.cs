using Domain.Entities.Spin;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface ISpinGameManager
{
    Task<Result> JoinGame(int userId, int gameId);

    Task<Result> InactivatePlayer(int userId, int gameId);

    Task<Result<int>> CreateGame(int userId, string name, Category? category = null);

    Task<Result<SpinGame>> StartGame(int userId, int gameId);

    Task<Result<SpinGame>> StartExistingGame(int userId, int gameId);
}