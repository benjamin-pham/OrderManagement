using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.DTOs;

public class AddOrderDetailRequest
{
    [MaxLength(255)]
    [Required]
    public string? ProductName { get; set; }
    [Range(0, int.MaxValue)]
    [Required]
    public int? Quantity { get; set; }
    [Range(0, double.PositiveInfinity)]
    [Required]
    public decimal? Price { get; set; }
}
