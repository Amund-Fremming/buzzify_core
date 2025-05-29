namespace Domain.Entities.Spin;

public sealed record Round(string ChallengeText, int RoundParticipants, IEnumerable<SpinPlayer> SelectedPlayers);