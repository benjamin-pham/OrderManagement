using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.DTOs;

public sealed record OrderCreateRequest
{
    [MaxLength(255)]
    [Required]
    public string? CustomerName { get; set; }
}
