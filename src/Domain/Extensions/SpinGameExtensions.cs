using Domain.Entities.Spin;

namespace Domain.Extensions;

public static class SpinGameExtensions
{
    public static SpinGame PartialCopy(this SpinGame spinGame, int hostId)
        => SpinGame.Create(
            category: spinGame.Category,
            name: spinGame.Name,
            hostId: hostId,
            challenges: spinGame.GetChallenges()
        ).AsCopy();
}