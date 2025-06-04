namespace Domain.Entities.Spin;

public sealed record Round(string ChallengeText, int RoundParticipants, List<SpinPlayer> AllPlayers, HashSet<SpinPlayer> SelectedPlayers);