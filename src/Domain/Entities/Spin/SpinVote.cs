using Domain.Abstractions;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Spin;

public class SpinVote : VoteBase, ITypeScriptModel
{
    public static SpinVote Create(int gameId, int playerId)
        => new()
        {
            GameId = gameId,
            PlayerId = playerId,
        };
}