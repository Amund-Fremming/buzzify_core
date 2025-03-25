using System.ComponentModel.DataAnnotations;

namespace Domain.Abstractions;

public abstract class VoteBase<T> where T : VoteBase<T>, new()
{
    [Key]
    public int Id { get; }

    public int GameId { get; private set; }
    public int PlayerId { get; private set; }
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