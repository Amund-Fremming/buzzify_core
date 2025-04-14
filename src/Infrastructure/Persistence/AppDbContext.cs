using Domain.Abstractions;
using Domain.Contracts;
using Domain.Entities.Ask;
using Domain.Entities.Shared;
using Domain.Entities.Spin;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    private const string _schema = "buzzify";

    public DbSet<UserBase> Users { get; set; }
    public DbSet<SpinGame> SpinGames { get; set; }
    public DbSet<SpinPlayer> SpinPlayers { get; set; }
    public DbSet<AskGame> AskGames { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Challenge> Challenges { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_schema);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserBase>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<GuestUser>(nameof(GuestUser))
            .HasValue<RegisteredUser>(nameof(RegisteredUser));

        modelBuilder.Entity<UserBase>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<SpinPlayer>()
            .HasIndex(p => new { p.UserId, p.GameId })
            .IsUnique();

        modelBuilder.Entity<GameBase>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<AskGame>()
            .HasBaseType<GameBase>();

        modelBuilder.Entity<SpinGame>()
            .HasBaseType<GameBase>();

        modelBuilder.Entity<SpinPlayer>()
            .HasOne(p => p.SpinGame)
            .WithMany(g => g.Players)
            .HasForeignKey(gp => gp.GameId);

        modelBuilder.Entity<Question>()
            .HasKey(q => q.Id);

        modelBuilder.Entity<Challenge>()
            .HasKey(c => c.Id);
    }

    public void ApplyChanges<T>(T entity) where T : class => base.Update(entity);

    public void Delete<T>(T entity) where T : class => base.Remove(entity);

    public async ValueTask<T?> FindAsync<T>(int id) where T : class => await base.Set<T>().FindAsync(id);

    public async ValueTask AddAsync<T>(T entity) where T : class => await base.AddAsync(entity);

    public async Task SaveChangesAsync() => await base.SaveChangesAsync();

    public DbSet<T> Entity<T>() where T : class => base.Set<T>();
}