using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class VoteBase
{
    [Key]
    public int Id { get; set; }

    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public bool Active { get; set; }
}