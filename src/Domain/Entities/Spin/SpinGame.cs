using Domain.Abstractions;
using Domain.Entities.Shared;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Spin;

public class SpinGame : GameBase, ITypeScriptModel
{
    public Category Category { get; set; }
    public SpinGameState State { get; set; }
    public string? HubGroupName { get; set; }
    public IList<SpinPlayer> Players { get; set; } = [];
    public IList<Challenge> Challenges { get; set; } = [];
    public IList<SpinVote> Votes { get; set; } = [];

    // May be bad for performance, could just do a db check
    // to see if any SpinGamePlayer instance has this playerId and gameId
    public Result AddPlayer(SpinPlayer player)
    {
        if (player is null)
        {
            return new Error("Player cannot be null.");
        }

        if (Players.Contains(player))
        {
            return new Error("Player has already joined the game.");
        }

        Players.Add(player);
        return Result.Ok;
    }

    public Result AddChallenge(Challenge challenge)
    {
        if (challenge is null)
        {
            return new Error("Challenge cannot be null.");
        }

        Challenges.Add(challenge);
        IterationsCount++;
        return Result.Ok;
    }

    public Result Upvote(SpinVote vote)
    {
        if (vote is null)
        {
            return new Error("Vote cannot be null.");
        }

        Votes.Add(vote);
        return Result.Ok;
    }

    // StartSpin
    // StartGame
    // NextRound

    public static SpinGame Create(string name, Player? creator = null, Category? category = Category.Random)
        => new()
        {
            Category = category ?? Category.Random,
            State = SpinGameState.Initialized,
            UniversalId = Guid.NewGuid(),
            CreatorId = creator?.Id ?? -1,
            Creator = creator ?? Player.Empty,
            Name = name,
            IterationsCount = 0,
            CurrentIteration = 0,
            Players = []
        };
}