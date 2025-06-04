using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class Challenge
{
    [Key]
    public int Id { get; }

    public int GameId { get; private set; }
    public int Participants { get; private set; }
    
    [MaxLength(100)]
    public string Text { get; private set; } = string.Empty;
    public bool ReadBeforeSpin { get; private set; }

    private Challenge()
    { }

    public Challenge EmptyText()
    {
        Text = string.Empty;
        return this;
    }

    public static Challenge Create(int gameId, int participants, string text, bool readBeforeSpin = false)
        => new()
        {
            GameId = gameId,
            Participants = participants,
            Text = text,
            ReadBeforeSpin = readBeforeSpin,
        };
}