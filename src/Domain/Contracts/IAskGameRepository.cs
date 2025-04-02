using Domain.Abstractions;
using Domain.Entities.Ask;
using Domain.Shared.ResultPattern;

namespace Domain.Contracts;

public interface IAskGameRepository : IRepository<AskGame>
{
    Task<Result<AskGame>> GetGameWithQuestions(int id);
}