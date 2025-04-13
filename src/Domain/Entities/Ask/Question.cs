using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Ask;

public class Question
{
    [Key]
    public int Id { get; }

    public int AskGameId { get; set; }
    public string Text { get; set; } = default!;

    public bool Active { get; set; }

    private Question()
    { }

    public static Question Create(int gameId, string text)
        => new()
        {
            Text = text,
            AskGameId = gameId,
            Active = true
        };
}