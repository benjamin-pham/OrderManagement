namespace OrderManagement.Domain.Orders;
public interface IOrderDetailRepository
{
    Task<OrderDetail?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Delete(OrderDetail orderDetail);
}
