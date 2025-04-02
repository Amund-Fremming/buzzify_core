using Domain.Abstractions;
using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class SpinPlayer : ITypeScriptModel
{
    [Key]
    public int Id { get; init; }

    public int SpinGameId { get; init; }
    public int PlayerId { get; init; }
    public bool Active { get; private set; }

    public SpinGame SpinGame { get; private set; } = default!;
    public IUser User { get; private set; } = default!;

    public static SpinPlayer Create(int spinGameId, int playerId, bool? host = default, bool? creator = default)
        => new()
        {
            SpinGameId = spinGameId,
            PlayerId = playerId,
        };
}