using Domain.Abstractions;
using Domain.Contracts;
using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Abstractions;

public class GenericRepository(IAppDbContext context) : IGenericRepository
{
    public async Task<Result<T>> GetById<T>(int id) where T : class
    {
        try
        {
            var entity = await context.Entity<T>()
                .FindAsync(id);

            if (entity == null)
            {
                return new EmptyResult();
            }

            return entity;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message, ex);
        }
    }

    public async Task<Result<PagedResponse<T>>> GetPage<T>(PagedRequest pagedRequest) where T : class
    {
        try
        {
            var count = await context.Entity<T>()
                .CountAsync();

            var data = await context.Entity<T>()
                .AsNoTracking()
                .Skip(pagedRequest.Skip)
                .Take(pagedRequest.Take)
                .ToListAsync();

            var page = PagedResponse<T>.Create(count, pagedRequest.PageNumber, pagedRequest.PageSize, data);
            return page;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message, ex);
        }
    }

    public async Task<Result> Create<T>(T entity) where T : class
    {
        try
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return Result.Ok;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message, ex);
        }
    }

    public async Task<Result> Update<T>(T entity) where T : class
    {
        try
        {
            context.ApplyChanges(entity);
            await context.SaveChangesAsync();
            return Result.Ok;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message, ex);
        }
    }

    public async Task<Result> Delete<T>(int id) where T : class
    {
        try
        {
            var result = await GetById<T>(id);
            if (result.IsError)
            {
                return result.Error;
            }

            var entity = result.Data;
            context.Delete(entity);
            await context.SaveChangesAsync();

            return Result.Ok;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message, ex);
        }
    }

    public async Task<Result<List<T>>> GetAll<T>() where T : class
    {
        try
        {
            var result = await context.Entity<T>()
                .ToListAsync();

            if (result is null)
            {
                return new List<T>();
            }

            return result;
        }
        catch (Exception ex)
        {
            return new Error(nameof(GetAll), ex);
        }
    }
}