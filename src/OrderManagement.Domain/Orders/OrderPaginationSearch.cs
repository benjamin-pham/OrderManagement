using OrderManagement.Domain.Abstractions;

namespace OrderManagement.Domain.Orders;
public sealed record OrderPaginationSearch : PagedListSearch
{
    public DateTime? FromCreatedAt { get; set; }
    public DateTime? ToCreatedAt { get; set; }
    public decimal? FromTotalAmount { get; set; }
    public decimal? ToTotalAmount { get; set; }
}
