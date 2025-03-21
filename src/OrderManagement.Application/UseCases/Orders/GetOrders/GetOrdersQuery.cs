using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;
using OrderManagement.Domain.Abstractions;

namespace OrderManagement.Application.UseCases.Orders.GetOrders;
public sealed record GetOrdersQuery : PagedListSearch, IQuery<PagedList<OrderResponse>>
{
    public DateTime? FromCreatedAt { get; set; }
    public DateTime? ToCreatedAt { get; set; }
    public decimal? FromTotalAmount { get; set; }
    public decimal? ToTotalAmount { get; set; }
}
