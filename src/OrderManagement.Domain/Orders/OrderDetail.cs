using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Orders;
public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; private set; }
    public string ProductName { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Order Order { get; private set; } = null!;
    private OrderDetail() { }
    public static OrderDetail Create(int orderId, string productName, int quantity, decimal price)
    {
        if (string.IsNullOrEmpty(productName))
            throw new AppException("ProductName cannot be null or empty");

        if (quantity < 1)
            throw new AppException("Quantity must be greater than zero");

        if (price < 1)
            throw new AppException("Price must be greater than zero");

        return new OrderDetail()
        {
            OrderId = orderId,
            ProductName = productName,
            Quantity = quantity,
            Price = price
        };
    }
}
