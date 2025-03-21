using OrderManagement.Domain.Orders;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.DTOs;

public sealed record UpdateOrderRequest
{
    [MaxLength(255)]
    [Required]
    public string? CustomerName { get; set; }
    [EnumDataType(typeof(OrderStatus))]
    [Required]
    public int? Status { get; set; }
}
