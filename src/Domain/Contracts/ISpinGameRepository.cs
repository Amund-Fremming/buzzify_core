﻿using Domain.Abstractions;
using Domain.Entities.Spin;
using Domain.Shared.ResultPattern;

namespace Domain.Contracts;

public interface ISpinGameRepository : IRepository<SpinGame>
{
    Task<Result<SpinGame>> GetGameWithChallenges(int id);
    Task<Result<SpinGame>> GetGameWithPlayersAndChallenges(int id);

    Task<Result<SpinGame>> GetGameWithPlayers(int id);

    Task<Result<SpinPlayer>> GetPlayer(int userId, int gameId);
}