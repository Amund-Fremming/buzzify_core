using Domain.Shared.TypeScript;

namespace Domain.Entities.Spin;

public class SpinRoundPlayer : ITypeScriptModel
{
    public int Id { get; set; }
    public int RoundId { get; set; }
    public int PlayerId { get; set; }

    public static SpinRoundPlayer Create(int roundId, int playerId)
        => new()
        {
            RoundId = roundId,
            PlayerId = playerId
        };
}