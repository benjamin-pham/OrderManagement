using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.Shared;
public sealed record OrderResponse
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
