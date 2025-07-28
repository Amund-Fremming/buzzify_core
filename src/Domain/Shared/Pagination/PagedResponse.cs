using Domain.Abstractions;

namespace Domain.Shared.Pagination;

public sealed record PagedResponse
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public List<GameBase> Data { get; set; } = default!;
    public int PageCount => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasNextPage => CurrentPage < PageCount;
    public bool HasPrevPage => CurrentPage > 1;

    private PagedResponse() { }

    public static PagedResponse Create<T>(int totalItems, int currentPage,int pageSize, List<T> data)
       => new()
       {
           TotalItems = totalItems,
           CurrentPage = currentPage,
           PageSize = pageSize,
           Data = data.Cast<GameBase>().ToList()
       };

    public static PagedResponse Empty()
        => new()
        {
            TotalItems = 0,
            CurrentPage = 0,
            PageSize = 0,
            Data = []
        };
}