using Domain.Abstractions;
using Domain.Entities.Shared;
using Domain.Shared.Enums;
using Domain.Shared.ResultPattern;

namespace Domain.Entities.Ask;

public class AskGame : GameBase
{
    public Category Category { get; set; }
    public AskGameState State { get; set; }
    public string? Description { get; set; }
    public IList<Question> Questions { get; set; } = [];

    public Result AddQuestion(string questionText)
    {
        if (string.IsNullOrEmpty(questionText))
        {
            return new Error("Parameter question cannot be null or empry.");
        }

        var question = Question.Create(questionText, Id);
        Questions.Add(question);
        IterationsCount++;
        return Result.Ok;
    }

    public Question NextQuestion() => throw new NotImplementedException();

    public void StartGame() => throw new NotImplementedException();

    public void NextRound() => throw new NotImplementedException();

    public static AskGame Create(string name, Player? creator = null, string? description = default!, Category? category = Category.Random)
        => new()
        {
            Category = category ?? Category.Random,
            State = AskGameState.Initialized,
            UniversalId = Guid.NewGuid(),
            CreatorId = creator?.Id ?? -1,
            Creator = creator ?? Player.Empty,
            Name = name,
            IterationsCount = 0,
            CurrentIteration = 0,
            Description = description,
            Questions = [],
        };
}