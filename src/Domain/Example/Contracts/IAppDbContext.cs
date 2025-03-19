using Core.src.Shared.ResultPattern;
using Domain.Example.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Example.Contracts;

public interface IAppDbContext
{
    DbSet<Result<ExampleEntity>> ExampleEntity { get; set; }

    Task<Result<T>> SaveChangesAsync<T>();

    Task<Result<T>> CreateAsync<T>(T entity);

    Task<Result<T>> FindAsync<T>(int id);

    Task<Result<IEnumerable<T>>> GetAllAsync<T>();

    Task<Result> DeleteAsync<T>();
}