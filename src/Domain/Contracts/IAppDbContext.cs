using Domain.Abstractions;
using Domain.Entities.Ask;
using Domain.Entities.Spin;
using Microsoft.EntityFrameworkCore;

namespace Domain.Contracts;

public interface IAppDbContext
{
    DbSet<UserBase> User { get; set; }
    DbSet<SpinGame> SpinGame { get; }
    DbSet<SpinPlayer> SpinPlayer { get; }
    DbSet<AskGame> AskGame { get; }
    DbSet<Question> Question { get; }
    DbSet<Challenge> Challenge { get; }

    DbSet<T> Entity<T>() where T : class;

    void ApplyChanges<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;

    ValueTask<T?> FindAsync<T>(int id) where T : class;

    ValueTask AddAsync<T>(T entity) where T : class;

    Task SaveChangesAsync();
}