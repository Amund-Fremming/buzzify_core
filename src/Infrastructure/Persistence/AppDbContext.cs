﻿using Domain.Abstractions;
using Domain.Contracts;
using Domain.Entities.Ask;
using Domain.Entities.Shared;
using Domain.Entities.Spin;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    private const string _schema = "Beer";

    public DbSet<UserBase> User { get; set; }
    public DbSet<SpinGame> SpinGame { get; set; }
    public DbSet<SpinPlayer> SpinPlayer { get; set; }
    public DbSet<AskGame> AskGame { get; set; }
    public DbSet<Question> Question { get; set; }
    public DbSet<Challenge> Challenge { get; set; }

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

        modelBuilder.Entity<AskGame>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<AskGame>()
            .ToTable(nameof(AskGame));

        modelBuilder.Entity<AskGame>()
            .HasIndex(g => new { g.Id, IsOriginal = g.IsCopy });

        modelBuilder.Entity<AskGame>()
            .Property(g => g.Name)
            .HasMaxLength(40);

        modelBuilder.Entity<AskGame>()
            .Property(g => g.Description)
            .HasMaxLength(100);

        modelBuilder.Entity<SpinGame>()
            .HasKey(g => g.Id);

        modelBuilder.Entity<SpinGame>()
            .ToTable(nameof(SpinGame));

        modelBuilder.Entity<SpinGame>()
            .HasIndex(g => new { g.Id, IsOriginal = g.IsCopy });
        
        modelBuilder.Entity<SpinGame>()
            .Property(g => g.Name)
            .HasMaxLength(40);  
        
        modelBuilder.Entity<SpinPlayer>()
            .HasOne(p => p.SpinGame)
            .WithMany(g => g.Players)
            .HasForeignKey(gp => gp.GameId);

        modelBuilder.Entity<Question>()
            .HasKey(q => q.Id);

        modelBuilder.Entity<Question>()
            .Property(q => q.Text)
            .HasMaxLength(100);

        modelBuilder.Entity<Challenge>()
            .HasKey(c => c.Id);
        
        modelBuilder.Entity<Challenge>()
            .Property(c => c.Text)
            .HasMaxLength(100); 
    }

    public void ApplyChanges<T>(T entity) where T : class => base.Update(entity);

    public void Delete<T>(T entity) where T : class => base.Remove(entity);

    public async ValueTask<T?> FindAsync<T>(int id) where T : class => await base.Set<T>().FindAsync(id);

    public async ValueTask AddAsync<T>(T entity) where T : class => await base.AddAsync(entity);

    public async Task SaveChangesAsync() => await base.SaveChangesAsync();

    public DbSet<T> Entity<T>() where T : class => base.Set<T>();
}