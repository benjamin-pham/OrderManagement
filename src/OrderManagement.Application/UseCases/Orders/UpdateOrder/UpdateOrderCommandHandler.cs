using AutoMapper;
using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UseCases.Orders.UpdateOrder;
internal sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<OrderResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

        if (order == null)
        {
            return Result.Failure<OrderResponse>("Order not found")!;
        }

        order.Update(request.CustomerName, request.Status);

        _orderRepository.Update(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<OrderResponse>(order);

        return Result.Success(response);
    }
}
