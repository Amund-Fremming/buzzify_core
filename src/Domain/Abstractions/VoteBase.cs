using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class VoteBase<T> where T : VoteBase<T>, new()
{
    [Key]
    public int Id { get; init; }

    public int GameId { get; init; }
    public int PlayerId { get; init; }
    public bool Active { get; private set; }

    public void SetActive(bool active) => Active = active;

    public static T Create(int gameId, int playerId)
        => new()
        {
            GameId = gameId,
            PlayerId = playerId,
            Active = true,
        };
}