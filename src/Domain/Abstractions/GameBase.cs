using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class GameBase
{
    [Key]
    public int Id { get; set; }

    public int UniversalId { get; set; }
    
    [MaxLength(40)]
    public string Name { get; set; } = null!;
    public int Iterations { get; set; }
    public int CurrentIteration { get; set; }
    public bool IsCopy { get; set;}

    public abstract void SetUniversalId();
}