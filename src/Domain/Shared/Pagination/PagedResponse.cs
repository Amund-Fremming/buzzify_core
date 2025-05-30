﻿namespace Domain.Shared.Pagination;

public sealed record PagedResponse<T>
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public IList<T> Data { get; set; } = default!;
    public int PageCount => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasNextPage => CurrentPage < PageCount;
    public bool HasPrevPage => CurrentPage > 1;

    private PagedResponse() { }

    public static PagedResponse<T> Create(int totalItems, int currentPage,int pageSize, IList<T> data)
       => new()
       {
           TotalItems = totalItems,
           CurrentPage = currentPage,
           PageSize = pageSize,
           Data = data
       };

    public static PagedResponse<T> Empty()
        => new()
        {
            TotalItems = 0,
            CurrentPage = 0,
            PageSize = 0,
            Data = []
        };
}