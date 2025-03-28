using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Ask;

public class Question : ITypeScriptModel
{
    [Key]
    public int Id { get; }

    public int AskGameId { get; init; }
    public string Text { get; init; } = default!;

    public bool Active { get; set; }

    private Question()
    { }

    public static Question Create(string text, int askGameId)
        => new()
        {
            Text = text,
            AskGameId = askGameId,
            Active = true
        };
}