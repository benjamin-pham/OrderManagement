using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Infrastructure.Repositories;
internal class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Task.FromResult<Order?>(null);

        return _dbContext.Set<Order>().SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public Task<Order?> GetByIdIncludeDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            return Task.FromResult<Order?>(null);

        return _dbContext.Set<Order>().Include(o => o.OrderDetails).SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public void Create(Order order)
    {
        _dbContext.Set<Order>().Add(order);
    }

    public void Delete(Order order)
    {
        _dbContext.Set<Order>().Remove(order);
    }    

    public void Update(Order order)
    {
        _dbContext.Set<Order>().Update(order);
    }
}
