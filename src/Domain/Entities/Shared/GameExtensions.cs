using Domain.Entities.Spin;

namespace Domain.Entities.Shared;

public static class GameExtensions
{
    public static SpinGame PartialCopy(this SpinGame spinGame)
           => SpinGame.Create(
               name: spinGame.Name,
               creator: spinGame.Creator,
               category: spinGame.Category);
}