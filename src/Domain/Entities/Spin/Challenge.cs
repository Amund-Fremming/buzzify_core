using Domain.Shared.TypeScript;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Spin;

public class Challenge : ITypeScriptModel
{
    [Key]
    public int Id { get; set; }

    public int RoundId { get; set; }
    public int ParticipantsCount { get; set; }
    public string Text { get; set; } = default!;
    public bool ReadBeforeSpin { get; set; }

    public static Challenge Create(int roundId, int participantsCount, string text, bool? readBeforeSpin = false)
        => new()
        {
            RoundId = roundId,
            ParticipantsCount = participantsCount,
            Text = text,
            ReadBeforeSpin = readBeforeSpin ?? false,
        };
}