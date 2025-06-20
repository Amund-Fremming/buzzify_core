﻿using Domain.Contracts;
using Domain.Entities.Spin;
using Domain.Shared.ResultPattern;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SpinGameRepository(IAppDbContext context) : RepositoryBase<SpinGame>(context), ISpinGameRepository
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<SpinGame>> GetGameWithPlayers(int id)
    {
        try
        {
            var game = await _context.SpinGame
                .Include(x => x.Players)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (game is null)
            {
                return new Error($"Game with id {id} does not exist.");
            }

            return game;
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetGameWithPlayers), ex);
        }
    }

    public async Task<Result<SpinGame>> GetGameWithChallenges(int id)
    {
        try
        {
            var result = await _context.SpinGame
                .Include(g => g.Challenges)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
            {
                return new Error($"Game with id {id} does not exist.");
            }
            
            return result;  
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetGameWithChallenges), ex);
        }
    }

    public async Task<Result<SpinGame>> GetGameWithPlayersAndChallenges(int id)
    {
        try
        {
            var game = await _context.SpinGame
                .Include(x => x.Players)
                .Include(x => x.Challenges)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

            if (game is null)
            {
                return new Error($"Game with id {id} does not exist.");
            }

            return game;
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetGameWithPlayersAndChallenges), ex);
        }
    }

    public async Task<Result<SpinPlayer>> GetPlayer(int userId, int gameId)
    {
        try
        {
            var player = await _context.SpinPlayer.FirstOrDefaultAsync(p => p.UserId == userId && p.GameId == gameId);
            if (player is null)
            {
                return new EmptyResult();
            }

            return player;
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetPlayer), ex);
        }
    }
}