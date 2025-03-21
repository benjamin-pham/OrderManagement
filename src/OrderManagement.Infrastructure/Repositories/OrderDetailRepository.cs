using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Infrastructure.Repositories;
internal class OrderDetailRepository : IOrderDetailRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderDetailRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Delete(OrderDetail orderDetail)
    {
        _dbContext.Set<OrderDetail>().Remove(orderDetail);
    }

    public Task<OrderDetail?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Set<OrderDetail>().SingleOrDefaultAsync(d => d.Id == id);
    }
}
