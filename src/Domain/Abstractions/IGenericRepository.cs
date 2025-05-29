using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;

namespace Domain.Abstractions;

public interface IGenericRepository
{
    Task<Result<T>> GetById<T>(int id) where T : class;

    Task<Result<PagedResponse<T>>> GetPage<T>(PagedRequest pagedRequest) where T : GameBase;

    Task<Result> Create<T>(T entity) where T : class;

    Task<Result> Update<T>(T entity) where T : class;

    Task<Result> Delete<T>(int id) where T : class;

    Task<Result<List<T>>> GetAll<T>() where T : class;
}