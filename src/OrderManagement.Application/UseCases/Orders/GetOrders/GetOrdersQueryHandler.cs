using AutoMapper;
using OrderManagement.Application.Abstractions.Messaging;
using OrderManagement.Application.Shared;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UseCases.Orders.GetOrders;
internal class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, PagedList<OrderResponse>>
{
    private readonly IOrderReadRepository _orderRepository;
    private readonly IMapper _mapper;
    public GetOrdersQueryHandler(
        IOrderReadRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedList<OrderResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        OrderPaginationSearch search = _mapper.Map<OrderPaginationSearch>(request);

        var data = await _orderRepository.GetPagination(search);

        return Result.Success(_mapper.Map<PagedList<OrderResponse>>(data));
    }
} 
