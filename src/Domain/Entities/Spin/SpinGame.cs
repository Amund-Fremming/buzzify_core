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

    private readonly IList<SpinPlayer> _players = [];
    private readonly IList<Challenge> _challenges = [];
    private readonly IList<SpinVote> _votes = [];

    public IReadOnlyList<SpinPlayer> Players => _players.AsReadOnly();
    public IReadOnlyList<Challenge> Challenges => _challenges.AsReadOnly();
    public IReadOnlyList<SpinVote> Votes { get; private set; } = [];

    private SpinGame()
    { }

    public Result AddPlayer(Player player, bool? isHost = false)
    {
        if (player is null)
        {
            return new Error("Player cannot be null.");
        }

        var spinPlayer = _players.FirstOrDefault(p => p.PlayerId == player.Id);
        if (spinPlayer is not null)
        {
            spinPlayer.Active = true;
        }

        if (spinPlayer is null)
        {
            _players.Add(SpinPlayer.Create(Id, player.Id, isHost));
        }

        return Result.Ok;
    }

    public Result AddChallenge(Challenge challenge)
    {
        if (challenge is null)
        {
            return new Error("Challenge cannot be null.");
        }

        _challenges.Add(challenge);
        IterationsCount++;
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

    public static SpinGame Create(string name, Player? creator = null, Category? category = Category.Random)
        => new()
        {
            Category = category ?? Category.Random,
            State = SpinGameState.Initialized,
            UniversalId = Guid.NewGuid(),
            CreatorId = creator?.Id ?? 0,
            Creator = creator ?? Player.Empty,
            Name = name,
        };
}