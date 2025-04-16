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

        if (State != SpinGameState.Initialized)
        {
            return new Error("Cannot add challenges in a started game.");
        }

        _challenges.Add(challenge);
        IterationCount++;
        return Result.Ok;
    }

    public Result<Challenge> StartSpin()
    {
        if (CurrentIteration > IterationCount || State == SpinGameState.Finished)
        {
            State = SpinGameState.Finished;
            return new Error("The game is finished.");
        }

        State = SpinGameState.Spinning;
        CurrentIteration++;
    }

    private Result<string> StartRound()
    {
        if (CurrentIteration > IterationCount || State == SpinGameState.Finished)
        {
            State = SpinGameState.Finished;
            return new Error("The game is finished.");
        }

        State = SpinGameState.RoundStarted;
        var challenge = _challenges[CurrentIteration - 1];
        if (!challenge.ReadBeforeSpin)
        {
            return "Neste challenge kommer da noen blir valgt, vær klare!";
        }

        return challenge.Text;
    }

    public SpinGame AsCopy()
    {
        State = SpinGameState.ChallengesClosed;
        return this;
    }

    public static SpinGame Create(string name, int hostId, Category? category = Category.Random)
        => new()
        {
            Category = category ?? Category.Random,
            State = SpinGameState.Initialized,
            UniversalId = $"{nameof(SpinGame)}:{Guid.NewGuid()}",
            Name = name,
            IterationCount = 0,
            CurrentIteration = 0,
            HostId = hostId,
        };
}