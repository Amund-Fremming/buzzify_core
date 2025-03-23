using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class RoundPlayer : ITypeScriptModel
{
    [Key]
    public int Id { get; set; }

    public int PlayerId { get; set; }
    public int RoundId { get; set; }
    public bool Host { get; set; }

    public static RoundPlayer Create(int playerId, int roundId, bool? host = false)
        => new()
        {
            PlayerId = playerId,
            RoundId = roundId,
            Host = host ?? false,
        };
}