namespace Domain.Shared.Pagination;

public sealed record PagedRequest<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}