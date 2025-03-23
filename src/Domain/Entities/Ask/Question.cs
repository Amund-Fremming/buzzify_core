using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Ask;

public class Question
{
    [Key]
    public int Id { get; set; }

    public int AskGameId { get; set; }

    public string Text { get; set; } = default!;

    public static Question Create(string text, int askGameId)
        => new()
        {
            Text = text,
            AskGameId = askGameId
        };
}