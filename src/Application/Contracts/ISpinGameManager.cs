using Domain.DTOs;
using Domain.Entities.Spin;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface ISpinGameManager
{
    Task<Result> JoinGame(int userId, int gameId);

    Task<Result<SpinPlayer>> InactivatePlayer(int userId, int gameId);

    Task<Result<CreateGameResponse>> CreateGame(int userId, string name, Category? category = null);

    Task<Result<SpinGame>> CreateGameCopy(int userId, int gameId);

    Task<Result<int>> AddChallenge(int gameId, int participants, string text, bool? readBeforeSpin = null);
    Task<Result<SpinGameState>> CloseChallenges(int userId, int gameId);  

    Task<Result<(string, SpinGameState)>> StartRound(int userId, int gameId);

    Task<Result<Round>> StartSpin(int userId, int gameId);
}