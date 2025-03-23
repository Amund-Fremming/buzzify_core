using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class GameBase
{
    [Key]
    public int Id { get; set; }

    public Guid UniversalId { get; set; }

    public string? CreatorId { get; set; }
    //public Player? Player { get; set; }

    public string? Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public int NumberOfIterations { get; set; }
    public int CurrentIterations { get; set; }
}