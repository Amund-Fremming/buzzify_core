using Domain.Abstractions;
using Domain.Extensions;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Domain.Entities.Ask;

/// <summary>
/// The game logic is entirely managed on the client side.
/// </summary>
public sealed class AskGame : GameBase
{
    public int CreatorId { get; set; }
    public Category Category { get; private set; }
    public AskGameState State { get; private set; }
    public string? Description { get; set; }

    private readonly List<Question> _questions = [];
    public IReadOnlyList<Question> Questions => _questions.AsReadOnly();

    private AskGame()
    { }

    public Result<int> AddQuestion(Question question)
    {
        if (question is null)
        {
            return new Error("Question cannot be null.");
        }

        _questions.Add(question);
        Iterations++;
        return Iterations;
    }

    public Result<AskGame> StartGame()
    {
        CurrentIteration = CurrentIteration + 1;
        State = AskGameState.Closed;
        _questions.Shuffle();
        return this;
    }

    public static AskGame Create(int userId, string name, string description = "", Category category = Category.Random)
    {
        var game = new AskGame()
        {
            CreatorId = userId,
            Category = category,
            State = AskGameState.Initialized,
            Name = name,
            Iterations = 0,
            CurrentIteration = 0,
            Description = description,
        };

        game.UniversalId = int.Parse("1" + game.Id);
        return game;
    }
}