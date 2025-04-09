using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class SpinPlayer
{
    [Key]
    public int Id { get; init; }

    public int GameId { get; init; }
    public int UserId { get; init; }
    private bool Active { get; set; }

    public SpinGame SpinGame { get; private set; } = default!;
    public IUser User { get; private set; } = default!;

    private SpinPlayer()
    { }

    public void SetActive(bool active) => Active = active;

    public static SpinPlayer Create(int spinGameId, int playerId, bool? host = default, bool? creator = default)
        => new()
        {
            GameId = spinGameId,
            UserId = playerId,
        };
}