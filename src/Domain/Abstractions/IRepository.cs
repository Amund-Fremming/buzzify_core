﻿using Domain.Shared.Pagination;
using Domain.Shared.ResultPattern;

namespace Domain.Abstractions;

public interface IRepository<T> where T : class
{
    Task<Result<T>> GetById(int id);

    Task<Result<PagedResponse>> GetPage(PagedRequest pagedRequest);

    Task<Result<IEnumerable<T>>> GetAll();

    Task<Result<T>> Create(T entity);

    Task<Result> Update(T entity);

    Task<Result> Delete(int id);
}