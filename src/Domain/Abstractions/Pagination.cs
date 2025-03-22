namespace Domain.Abstractions;

public sealed record Pagination<T> where T : class
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T>? Items { get; set; }
    public bool HasNextPage
    {
        get;
    }
    public bool HasPrevPage => PageSize > 0;
}