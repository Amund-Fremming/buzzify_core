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
    public bool Upvote { get; set; }
    public bool Saved { get; set; }
    public IUser User { get; init; } = default!;
    public GameBase Game { get; init; } = default!;

    public void UpdateSaved(bool saved) => Saved = saved;

    public void UpdateUpvote(bool upvote) => Upvote = upvote;

    public static Vote Create(int gameId, int userId, string gameType, bool? upvote, bool? saved)
        => new()
        {
            GameId = gameId,
            UserId = userId,
            GameType = gameType,
            Upvote = upvote ?? false,
            Saved = saved ?? false,
        };
}