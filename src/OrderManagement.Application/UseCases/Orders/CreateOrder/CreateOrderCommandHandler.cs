using AutoMapper;
using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UseCases.Orders.CreateOrder;
internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = Order.Create(request.CustomerName);

        _orderRepository.Create(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = _mapper.Map<OrderResponse>(order);

        return Result.Success(response);
    }
}
