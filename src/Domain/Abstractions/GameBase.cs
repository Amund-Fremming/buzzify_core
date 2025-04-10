using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class GameBase
{
    [Key]
    public int Id { get; init; }

    public string? UniversalId { get; init; }
    public string Name { get; init; } = default!;
    public int IterationCount { get; set; }
    public int CurrentIteration { get; set; }
}