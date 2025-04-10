using Domain.Abstractions;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Domain.Entities.Spin;

public sealed class SpinGame : GameBase
{
    public Category Category { get; private set; }
    public SpinGameState State { get; private set; }
    public string? HubGroupName { get; private set; }
    public int HostId { get; private set; }
    public UserBase Host { get; private set; } = default!;

    private readonly IList<SpinPlayer> _players = [];
    private readonly IList<Challenge> _challenges = [];

    public IReadOnlyList<SpinPlayer> Players => _players.AsReadOnly();
    public IReadOnlyList<Challenge> Challenges => _challenges.AsReadOnly();

    private SpinGame()
    { }

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

    // StartSpin
    // StartGame
    // NextRound

    public SpinGame AsCopy()
    {
        State = SpinGameState.ChallengesClosed;
        return this;
    }

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