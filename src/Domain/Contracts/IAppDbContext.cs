using Microsoft.EntityFrameworkCore;

namespace Domain.Contracts;

public interface IAppDbContext
{
    DbSet<T> Entity<T>() where T : class;

    void ApplyChanges<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;

    ValueTask<T?> FindAsync<T>(int id) where T : class;

    ValueTask AddAsync<T>(T entity) where T : class;

    Task SaveChangesAsync();
}