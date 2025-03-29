using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Shared;

public class Vote
{
    [Key]
    public int Id { get; init; }

    public int GameId { get; init; }
    public int UserId { get; init; }
    public string GameType { get; init; } = string.Empty;
    public bool Active { get; set; }
    public IUser User { get; init; } = default!;
    public GameBase Game { get; init; } = default!;
}