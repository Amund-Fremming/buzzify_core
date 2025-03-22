using Domain.Abstractions;
using Domain.Shared.ResultPattern;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions;

public class GenericRepository(AppDbContext context) : IGenericRepository
{
    public async Task<Result<T>> GetByUniversalId<T>(Guid universalId) where T : class
    {
        try
        {
            var entity = await context.Set<T>()
                .FindAsync(universalId);

            if (entity != null)
            {
                return entity;
            }

            return new Error($"{typeof(T)} with id {universalId}, does not exist.");
        }
        catch (Exception ex)
        {
            return new Error(ex.Message, ex);
        }
    }

    public async Task<Result<IEnumerable<T>>> GetAll<T>() where T : class
    {
        try
        {
            return await context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            return new Error(ex.Message, ex);
        }
    }
}