using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UseCases.Orders.RemoveOrderDetail;
internal sealed class RemoveOrderDetailCommandHandler : ICommandHandler<RemoveOrderDetailCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RemoveOrderDetailCommandHandler(
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveOrderDetailCommand request, CancellationToken cancellationToken)
    {
        OrderDetail? orderDetail = await _orderDetailRepository.GetByIdAsync(request.Id, cancellationToken);

        if (orderDetail is null)
            return Result.Failure();

        Order? order = await _orderRepository.GetByIdIncludeDetailsAsync(orderDetail.OrderId, cancellationToken);

        if (order is null)
            return Result.Failure();

        order.RemoveDetail(orderDetail.Id);

        _orderRepository.Update(order);

        _orderDetailRepository.Delete(orderDetail);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
