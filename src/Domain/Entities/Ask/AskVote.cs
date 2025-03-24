using Domain.Abstractions;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Ask;

public class AskVote : VoteBase, ITypeScriptModel
{
    public static AskVote Create(int gameId, int playerId)
        => new()
        {
            GameId = gameId,
            PlayerId = playerId,
        };
}