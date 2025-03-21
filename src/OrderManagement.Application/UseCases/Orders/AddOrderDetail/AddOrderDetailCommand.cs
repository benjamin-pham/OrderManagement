using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;

namespace OrderManagement.Application.UseCases.Orders.AddOrderDetail;
public sealed record AddOrderDetailCommand(int orderId, string ProductName, int Quantity, decimal Price) : ICommand;
