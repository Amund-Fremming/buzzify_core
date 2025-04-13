using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class GameBase
{
    [Key]
    public int Id { get; set; }

    public string? UniversalId { get; set; }
    public string Name { get; set; } = default!;
    public int IterationCount { get; set; }
    public int CurrentIteration { get; set; }
}