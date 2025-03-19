using Core.src.Shared.ResultPattern;
using Domain.Example.Contracts;
using Domain.Example.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Result<ExampleEntity>> ExampleEntity { get; set; }

    public async Task<Result<ExampleEntity>> CreateAsync(ExampleEntity entity)
    {
        try
        {
            var createdEntity = await ExampleEntities.AddAsync(entity);
            await SaveChangesAsync();
            return Result<ExampleEntity>.Success(createdEntity.Entity); // Returner den opprettede entiteten
        }
        catch (Exception ex)
        {
            return Result<ExampleEntity>.Failure($"Error while creating entity: {ex.Message}");
        }
    }

    public async Task<Result<ExampleEntity>> FindByIdAsync(int id)
    {
        try
        {
            var entity = await ExampleEntities.FindAsync(id);
            if (entity == null)
            {
                return Result<ExampleEntity>.Failure("Entity not found");
            }
            return Result<ExampleEntity>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<ExampleEntity>.Failure($"Error while finding entity: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<ExampleEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await ExampleEntities.ToListAsync();
            return Result<IEnumerable<ExampleEntity>>.Success(entities);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ExampleEntity>>.Failure($"Error while retrieving all entities: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<ExampleEntity>>> GetSliceAsync(int skip, int take)
    {
        try
        {
            var entities = await ExampleEntities.Skip(skip).Take(take).ToListAsync();
            return Result<IEnumerable<ExampleEntity>>.Success(entities);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<ExampleEntity>>.Failure($"Error while retrieving entity slice: {ex.Message}");
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var entity = await ExampleEntities.FindAsync(id);
            if (entity == null)
            {
                return Result.Failure("Entity not found");
            }

            ExampleEntities.Remove(entity);
            await SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error while deleting entity: {ex.Message}");
        }
    }

    // Implementering av SaveChangesAsync
    public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            await base.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error while saving changes: {ex.Message}");
        }
    }
}