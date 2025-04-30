using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class SpinPlayer
{
    [Key]
    public int Id { get; init; }

    public int GameId { get; init; }
    public int UserId { get; init; }
    public int TimesChosen { get; set; } = 0;
    public bool Active { get; private set; }

    public SpinGame SpinGame { get; private set; } = default!;
    public UserBase User { get; private set; } = default!;

    private SpinPlayer()
    { }

    public void Chosen() => TimesChosen++;

    public void SetActive(bool active) => Active = active;

    public static SpinPlayer Create(int gameId, int userId)
        => new()
        {
            GameId = gameId,
            UserId = userId,
            Active = true,
        };
}