
using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;

namespace OrderManagement.Application.UseCases.Orders.CreateOrder;
public record CreateOrderCommand(string CustomerName) : ICommand<OrderResponse>;