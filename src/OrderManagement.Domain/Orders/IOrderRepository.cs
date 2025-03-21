namespace OrderManagement.Domain.Orders;
public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdIncludeDetailsAsync(int id, CancellationToken cancellationToken = default);
    void Create(Order order);
    void Update(Order order);
    void Delete(Order order);
}
