using Dapper;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;
using OrderManagement.Infrastructure.Data;

namespace OrderManagement.Infrastructure.ReadRepository;
internal class OrderReadRepository : IOrderReadRepository
{
    private readonly SqlConnectionFactory _connectionFactory;
    public OrderReadRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public Task<Order?> GetByIdAsync(int id)
    {
        if (id <= 0)
            return Task.FromResult<Order?>(null);

        return _connectionFactory
            .CreateConnection()
            .QueryFirstOrDefaultAsync<Order>(
            sql: "SELECT * FROM Orders WHERE Id = @Id",
            param: new { Id = id },
            commandType: System.Data.CommandType.Text);
    }

    public Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderId(int orderId)
    {
        if (orderId <= 0)
            return Task.FromResult<IEnumerable<OrderDetail>>([]);

        return _connectionFactory
            .CreateConnection()
            .QueryAsync<OrderDetail>(
            sql: "SELECT * FROM OrderDetails WHERE OrderId = @OrderId",
            param: new { OrderId = orderId },
            commandType: System.Data.CommandType.Text);
    }

    public async Task<PagedList<Order>> GetPagination(OrderPaginationSearch filter)
    {
        var gridReader = await _connectionFactory
            .CreateConnection()
            .QueryMultipleAsync(
            sql: "GetOrders_Pagination", // scripts.sql
            param: filter,
            commandType: System.Data.CommandType.StoredProcedure);

        int totalResults = await gridReader.ReadFirstOrDefaultAsync<int>();

        if (totalResults == 0)
            return PagedList<Order>.Create([], filter.PageIndex, filter.PageSize, 0);

        IEnumerable<Order> results = await gridReader.ReadAsync<Order>();

        return PagedList<Order>.Create(results, filter.PageIndex, filter.PageSize, totalResults);
    }
}
