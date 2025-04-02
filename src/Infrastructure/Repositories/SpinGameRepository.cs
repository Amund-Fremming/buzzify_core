using Domain.Contracts;
using Domain.Entities.Ask;
using Domain.Entities.Spin;
using Domain.Shared.ResultPattern;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SpinGameRepository(IAppDbContext context) : RepositoryBase<SpinGame>(context), ISpinGameRepository
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<AskGame>> GetGameWithPlayersAndChallenges(int id)
    {
        try
        {
            var game = await _context.SpinGames
                .Include(x => x.Players)
                .Include(x => x.Challenges)
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
        };
    }
}