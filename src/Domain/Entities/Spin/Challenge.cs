using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class Challenge
{
    [Key]
    public int Id { get; set; }

    public int RoundId { get; set; }
    public int NumberOfPlayers { get; set; }
    public string Text { get; set; } = default!;
    public bool ReadBeforeSpin { get; set; }

    // Maybe remove??
    public int Weight { get; set; }
}