using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class SpinPlayer : ITypeScriptModel
{
    [Key]
    public int Id { get; }

    public int SpinGameId { get; private set; }
    public int PlayerId { get; private set; }
    public bool Host { get; private set; }

    public void SetHost(bool host) => Host = host;

    public static SpinPlayer Create(int spinGameId, int playerId, bool? host = default)
        => new()
        {
            SpinGameId = spinGameId,
            PlayerId = playerId,
            Host = host ?? false,
        };
}