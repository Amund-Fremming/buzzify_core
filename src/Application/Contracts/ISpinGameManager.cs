﻿using Domain.Entities.Spin;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Application.Contracts;

public interface ISpinGameManager
{
    Task<Result> JoinGame(int userId, int gameId);

    Task<Result<SpinPlayer>> InactivatePlayer(int userId, int gameId);

    Task<Result<int>> CreateGame(int userId, string name, Category? category = null);

    Task<Result<SpinGame>> CreateExistingGame(int userId, int gameId);

    Task<Result<int>> AddChallenge(int gameId, int participants, string text, bool readBeforeSpin = false);

    Task<Result<string>> StartRound(int userId, int gameId);

    Task<Result<Challenge>> StartSpin(int userId, int gameId);
}