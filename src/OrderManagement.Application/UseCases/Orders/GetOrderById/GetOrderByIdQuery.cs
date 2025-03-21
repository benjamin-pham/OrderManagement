using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;

namespace OrderManagement.Application.UseCases.Orders.GetOrderById;
public sealed record GetOrderByIdQuery(int Id) : IQuery<OrderResponse>;