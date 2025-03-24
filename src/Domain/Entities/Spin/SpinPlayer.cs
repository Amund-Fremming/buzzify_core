using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class SpinPlayer : ITypeScriptModel
{
    [Key]
    public int Id { get; set; }

    public int SpinGameId { get; set; }
    public int PlayerId { get; set; }
    public bool Host { get; set; }

    public static SpinPlayer Create(int spinGameId, int playerId, bool? host = default)
        => new()
        {
            SpinGameId = spinGameId,
            PlayerId = playerId,
            Host = host ?? false,
        };
}