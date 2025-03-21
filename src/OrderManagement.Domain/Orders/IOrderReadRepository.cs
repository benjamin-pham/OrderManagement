using OrderManagement.Domain.Abstractions;

namespace OrderManagement.Domain.Orders;
public interface IOrderReadRepository
{
    Task<Order?> GetByIdAsync(int id);
    Task<PagedList<Order>> GetPagination(OrderPaginationSearch filter);
    Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderId(int orderId);
}
