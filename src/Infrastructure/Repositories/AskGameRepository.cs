using Domain.Contracts;
using Domain.Entities.Ask;
using Domain.Shared.ResultPattern;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AskGameRepository(IAppDbContext context) : RepositoryBase<AskGame>(context), IAskGameRepository
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<AskGame>> GetGameWithQuestions(int id)
    {
        try
        {
            var game = await _context.AskGame
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game is null)
            {
                return new Error($"Game with id {id} does not exist.");
            }

            return game;
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetGameWithQuestions), ex);
        }
    }
}