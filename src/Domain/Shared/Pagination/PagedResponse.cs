namespace Domain.Shared.Pagination;

public sealed record PagedResponse<T>
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Data { get; set; } = default!;
    public int PageCount => (int)Math.Ceiling((double)TotalItems / PageSize);
    public bool HasNextPage => CurrentPage < PageCount;
    public bool HasPrevPage => PageSize > 0;
}