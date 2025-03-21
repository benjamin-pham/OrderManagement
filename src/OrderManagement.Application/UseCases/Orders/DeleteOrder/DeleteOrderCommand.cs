
using OrderManagement.Application.Abstractions.Messaging;

namespace OrderManagement.Application.UseCases.Orders.DeleteOrder;
public sealed record DeleteOrderCommand(int Id) : ICommand;
