using Domain.Abstractions;
using Domain.Entities.Shared;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Spin;

public sealed class SpinGame : GameBase, ITypeScriptModel
{
    public Category Category { get; private set; }
    public SpinGameState State { get; private set; }
    public string? HubGroupName { get; private set; }
    public int HostId { get; private set; }
    public IUser Host { get; private set; } = default!;

    private readonly IList<SpinPlayer> _players = [];
    private readonly IList<Challenge> _challenges = [];

    public IReadOnlyList<SpinPlayer> Players => _players.AsReadOnly();
    public IReadOnlyList<Challenge> Challenges => _challenges.AsReadOnly();

    public Result AddChallenge(Challenge challenge)
    {
        if (challenge is null)
        {
            return new Error("Challenge cannot be null.");
        }

        _challenges.Add(challenge);
        IterationCount++;
        return Result.Ok;
    }

    public Result Upvote(Vote vote)
    {
        if (vote is null)
        {
            return new Error("Vote cannot be null.");
        }

        _votes.Add(vote);
        return Result.Ok;
    }

    // StartSpin
    // StartGame
    // NextRound

    public static SpinGame Create(string name, int hostId, Category? category = Category.Random, int? iterationCount = 0, int? currentIteration = 0)
        => new()
        {
            Category = category ?? Category.Random,
            State = SpinGameState.Initialized,
            UniversalId = $"{nameof(SpinGame)}:{Guid.NewGuid()}",
            Name = name,
            IterationCount = iterationCount ?? 0,
            CurrentIteration = currentIteration ?? 0,
            HostId = hostId,
        };
}