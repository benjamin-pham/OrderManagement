using OrderManagement.Application.Abstractions.Messaging;

namespace OrderManagement.Application.UseCases.Orders.RemoveOrderDetail;
public sealed record RemoveOrderDetailCommand(int Id) : ICommand;