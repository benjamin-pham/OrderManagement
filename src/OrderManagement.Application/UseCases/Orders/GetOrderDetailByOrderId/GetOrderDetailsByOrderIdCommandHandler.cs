using AutoMapper;
using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UseCases.Orders.GetOrderDetailByOrderId;
internal class GetOrderDetailsByOrderIdCommandHandler : ICommandHandler<GetOrderDetailsByOrderIdCommand, IEnumerable<OrderDetailResponse>>
{
    private readonly IOrderReadRepository _orderRepository;
    private readonly IMapper _mapper;
    public GetOrderDetailsByOrderIdCommandHandler(
        IOrderReadRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<OrderDetailResponse>>> Handle(GetOrderDetailsByOrderIdCommand request, CancellationToken cancellationToken)
    {
        var data = await _orderRepository.GetOrderDetailByOrderId(request.OrderId);

        var response = _mapper.Map<IEnumerable<OrderDetailResponse>>(data);

        return Result.Success(response);
    }
}
