using Domain.Entities.Spin;

namespace Domain.Extentions;

public static class GameExtensions
{
    public static SpinGame PartialCopy(this SpinGame spinGame, int hostId)
        => SpinGame.Create(
            category: spinGame.Category,
            name: spinGame.Name,
            hostId: hostId
        ).AsCopy();
}