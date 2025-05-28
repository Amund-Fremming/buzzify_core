using Domain.Abstractions;
using Domain.Extensions;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Domain.Entities.Spin;

public sealed class SpinGame : GameBase
{
    public Category Category { get; private set; }
    public SpinGameState State { get; private set; }
    public int HostId { get; private set; }
    public UserBase Host { get; private set; } = null!;

    private readonly IList<SpinPlayer> _players = [];
    private readonly IList<Challenge> _challenges = [];

    public IReadOnlyList<SpinPlayer> Players => _players.AsReadOnly();
    public IReadOnlyList<Challenge> Challenges => _challenges.AsReadOnly();

    private SpinGame()
    { }

    public Result AddPlayer(SpinPlayer player)
    {
        if (_players.Contains(player))
        {
            return new Error("Spilleren er allerede med i spillet");
        }

        _players.Add(player);
        return Result.Ok;
    }

    public Result<SpinPlayer> UpdateHost()
    {
        var prevHost = _players.FirstOrDefault(p => p.Id == HostId);
        prevHost?.SetActive(false);

        if (_players.Count == 0)
        {
            return new Error("The game does not have any players left.");
        }

        var nextHost = _players[0];
        HostId = nextHost.Id;
        return nextHost;
    }

    public Result<int> AddChallenge(Challenge challenge)
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
        Iterations++;
        return Iterations;
    }

    public Result<IEnumerable<SpinPlayer>> GetChosenPlayers(int numberOfPlayers)
    {
        if (_players.Count == 0)
        {
            return new Error("Spillet er tomt");
        }
        
        var playersMap = _players.ToDictionary(p => p.Id, p => p);
        var weightedList = _players.Select(p => (p, (1 - p.TimesChosen / CurrentIteration)))
            .ToList()
            .Shuffle();

        var random = new Random();
        var r = random.NextDouble() * 1;

        var chosenPlayers = new List<SpinPlayer>();
        while (numberOfPlayers > 0)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var
                if()
            }
        }
        

        return chosenPlayers;
    }

    public Result<Challenge> StartSpin()
    {
        if (CurrentIteration > Iterations || State == SpinGameState.Finished)
        {
            State = SpinGameState.Finished;
            return new Error("The game is finished.");
        }

        State = SpinGameState.Spinning;
        CurrentIteration++;
        var challenge = _challenges[CurrentIteration - 1];
        if (!challenge.ReadBeforeSpin)
        {
            return challenge;
        }

        return challenge.EmptyText();
    }

    public Result<string> StartRound()
    {
        if (CurrentIteration > Iterations || State == SpinGameState.Finished)
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

    public bool GameFinished() => State == SpinGameState.Finished || CurrentIteration == Iterations - 1;

    public SpinGame AsCopy()
    {
        State = SpinGameState.ChallengesClosed;
        return this;
    }

    public static SpinGame Create(string name, int hostId, Category? category = Category.Random)
    {
        var game = new SpinGame()
        {
            Category = category ?? Category.Random,
            State = SpinGameState.Initialized,
            Name = name,
            Iterations = 0,
            CurrentIteration = 0,
            HostId = hostId,
        };

        game.UniversalId = int.Parse("2" + game.Id);
        return game;
    }
}