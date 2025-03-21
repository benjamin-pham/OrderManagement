namespace OrderManagement.Domain.Abstractions;
public abstract record PagedListSearch
{
    private int _pageIndex = 1;
    private int _pageSize = 20;
    public int PageIndex
    {
        get => _pageIndex;
        set
        {
            if (value > 0)
                _pageIndex = value;
        }
    }
    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value > 0)
                _pageSize = value;
        }
    }
    private string? _searchTerm;
    public string? SearchTerm
    {
        get
        {
            return _searchTerm;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
                _searchTerm = value!.Trim();
        }
    }
}
