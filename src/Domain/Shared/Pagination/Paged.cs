namespace Domain.Shared.Pagination;

public static class Paged
{
    public static PagedResponse<T> Create<T>(int totalItems, int currentPage, int pageSize, IEnumerable<T> data)
        => new()
        {
            TotalItems = totalItems,
            CurrentPage = currentPage,
            PageSize = pageSize,
            Data = data
        };

    public static PagedResponse<T> Empty<T>()
        => new()
        {
            TotalItems = 0,
            CurrentPage = 0,
            PageSize = 0,
            Data = []
        };
}