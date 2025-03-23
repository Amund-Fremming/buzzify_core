using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class SpinGamePlayer : ITypeScriptModel
{
    [Key]
    public int Id { get; set; }

    public int GameId { get; set; }
    public int PlayerId { get; set; }

    public static SpinGamePlayer Create(int gameId, int playerId)
        => new()
        {
            GameId = gameId,
            PlayerId = playerId
        };
}