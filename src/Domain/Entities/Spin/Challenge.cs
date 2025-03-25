using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class Challenge : ITypeScriptModel
{
    [Key]
    public int Id { get; }

    public int RoundId { get; private set; }
    public int ParticipantsCount { get; private set; }
    public string Text { get; private set; } = default!;
    public bool ReadBeforeSpin { get; private set; }

    public static Challenge Create(int roundId, int participantsCount, string text, bool? readBeforeSpin = false)
        => new()
        {
            RoundId = roundId,
            ParticipantsCount = participantsCount,
            Text = text,
            ReadBeforeSpin = readBeforeSpin ?? false,
        };
}