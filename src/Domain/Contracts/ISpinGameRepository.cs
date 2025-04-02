using Domain.Abstractions;
using Domain.Entities.Ask;
using Domain.Entities.Spin;
using Domain.Shared.ResultPattern;

namespace Domain.Contracts;

public interface ISpinGameRepository : IRepository<SpinGame>
{
    Task<Result<AskGame>> GetGameWithPlayersAndChallenges(int id);
}