namespace OrderManagement.Domain.Abstractions;
public sealed class PagedList<TResult>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalResults { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage => PageIndex < TotalPages;
    public bool HasPreviousPage => PageIndex > 1;
    public List<TResult> Results { get; set; } = [];
    public static PagedList<TResult> Create(IEnumerable<TResult> results, int pageIndex, int pageSize, int totalResults)
    {
        return new PagedList<TResult>
        {
            Results = results.ToList(),
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalResults = totalResults,
            TotalPages = (int)Math.Ceiling((decimal)totalResults / pageSize)
        };
    }
}
