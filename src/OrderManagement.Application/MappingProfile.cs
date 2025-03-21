using AutoMapper;
using OrderManagement.Application.Shared;
using OrderManagement.Application.UseCases.Orders.GetOrders;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application;
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderResponse>();

        CreateMap<PagedList<Order>, PagedList<OrderResponse>>();

        CreateMap<GetOrdersQuery, OrderPaginationSearch>();

        CreateMap<OrderDetail, OrderDetailResponse>();
    }
}
