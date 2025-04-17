using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class Challenge
{
    [Key]
    public int Id { get; }

    public int RoundId { get; private set; }
    public int Participants { get; private set; }
    public string Text { get; private set; } = default!;
    public bool ReadBeforeSpin { get; private set; }

    private Challenge()
    { }

    public Challenge EmptyText()
    {
        Text = string.Empty;
        return this;
    }

    public static Challenge Create(int roundId, int participants, string text, bool? readBeforeSpin = false)
        => new()
        {
            RoundId = roundId,
            Participants = participants,
            Text = text,
            ReadBeforeSpin = readBeforeSpin ?? false,
        };
}