using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UseCases.Orders.UpdateOrder;
public sealed record UpdateOrderCommand(int Id, string CustomerName, OrderStatus Status) : ICommand<OrderResponse>;