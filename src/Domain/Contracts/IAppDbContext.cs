using Domain.Abstractions;
using Domain.Entities.Ask;
using Domain.Entities.Shared;
using Domain.Entities.Spin;
using Microsoft.EntityFrameworkCore;

namespace Domain.Contracts;

public interface IAppDbContext
{
    DbSet<UserBase> Users { get; set; }
    DbSet<SpinGame> SpinGames { get; }
    DbSet<SpinPlayer> SpinPlayers { get; }
    DbSet<Vote> Votes { get; }
    DbSet<AskGame> AskGames { get; }

    DbSet<T> Entity<T>() where T : class;

    void ApplyChanges<T>(T entity) where T : class;

    void Delete<T>(T entity) where T : class;

    ValueTask<T?> FindAsync<T>(int id) where T : class;

    ValueTask AddAsync<T>(T entity) where T : class;

    Task SaveChangesAsync();
}