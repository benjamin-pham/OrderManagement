using AutoMapper;
using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UseCases.Orders.GetOrderById;
internal class GetOrderByIdQueryQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
    private readonly IOrderReadRepository _orderRepository;
    private readonly IMapper _mapper;
    public GetOrderByIdQueryQueryHandler(
        IOrderReadRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _orderRepository.GetByIdAsync(request.Id);

        if (data is null)
            return Result.Failure<OrderResponse>()!;

        return Result.Success(_mapper.Map<OrderResponse>(data));
    }
}
