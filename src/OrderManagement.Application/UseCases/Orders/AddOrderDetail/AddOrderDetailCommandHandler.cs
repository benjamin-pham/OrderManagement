using AutoMapper;
using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UseCases.Orders.AddOrderDetail;
public sealed class AddOrderDetailCommandHandler : ICommandHandler<AddOrderDetailCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AddOrderDetailCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(AddOrderDetailCommand request, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetByIdIncludeDetailsAsync(request.orderId);

        if (order is null)
            return Result.Failure<OrderDetailResponse>()!;

        order.AddDetail(request.ProductName, request.Quantity, request.Price);

        _orderRepository.Update(order);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
