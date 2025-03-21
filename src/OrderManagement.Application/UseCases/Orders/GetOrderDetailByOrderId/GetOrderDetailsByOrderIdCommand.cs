using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;

namespace OrderManagement.Application.UseCases.Orders.GetOrderDetailByOrderId;
public sealed record GetOrderDetailsByOrderIdCommand(int OrderId) : ICommand<IEnumerable<OrderDetailResponse>>;