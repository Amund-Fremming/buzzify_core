using Domain.Abstractions;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;
using Domain.Shared.TypeScript;

namespace Domain.Entities.Spin;

public sealed class SpinGame : GameBase, ITypeScriptModel
{
    public Category Category { get; private set; }
    public SpinGameState State { get; private set; }
    public string? HubGroupName { get; private set; }

    private readonly IList<SpinPlayer> _players = [];
    private readonly IList<Challenge> _challenges = [];
    private readonly IList<SpinVote> _votes = [];

    public IReadOnlyList<SpinPlayer> Players => _players.AsReadOnly();
    public IReadOnlyList<Challenge> Challenges => _challenges.AsReadOnly();
    public IReadOnlyList<SpinVote> Votes => _votes.AsReadOnly();

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

    public Result Upvote(SpinVote vote)
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
    public SpinGame PartialCopy() => Create(base.Name, Category);

    public static SpinGame Create(string name, Category? category = Category.Random, int? iterationCount = 0, int? currentIteration = 0)
        => new()
        {
            Category = category ?? Category.Random,
            State = SpinGameState.Initialized,
            UniversalId = Guid.NewGuid(),
            Name = name,
            IterationCount = iterationCount ?? 0,
            CurrentIteration = currentIteration ?? 0,
        };
}