using Domain.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class GameBase
{
    [Key]
    public int Id { get; set; }

    public Guid UniversalId { get; set; }
    public int CreatorId { get; set; }
    public Player? Creator { get; set; }
    public string Name { get; set; } = default!;
    public int IterationsCount { get; set; }
    public int CurrentIteration { get; set; }
}