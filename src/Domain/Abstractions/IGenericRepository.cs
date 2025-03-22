using Domain.Shared.ResultPattern;

namespace Domain.Abstractions;

public interface IGenericRepository
{
    Task<Result<T>> GetByUniversalId<T>(Guid universalId) where T : class;

    Task<Result<IEnumerable<T>>> GetAll<T>() where T : class;
}