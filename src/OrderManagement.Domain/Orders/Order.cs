using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Domain.Orders;
public class Order : IAuditable
{
    public int Id { get; set; }
    public string CustomerName { get; private set; } = null!;
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    private readonly List<OrderDetail> _orderDetails = new();

    public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    private Order() { }

    public static Order Create(string customerName)
    {
        if (string.IsNullOrEmpty(customerName))
            throw new AppException("CustomerName cannot be null or empty");

        return new Order()
        {
            CustomerName = customerName,
            Status = OrderStatus.Pending
        };
    }

    public void Update(string customerName, OrderStatus status)
    {
        if (string.IsNullOrEmpty(customerName))
            throw new AppException("CustomerName cannot be null or empty");

        CustomerName = customerName;
        Status = status;
    }

    public void AddDetail(string productName, int quantity, decimal price)
    {
        _orderDetails.Add(OrderDetail.Create(Id, productName, quantity, price));

        CalculateTotalAmount();
    }

    public void RemoveDetail(int id)
    {
        var detail = _orderDetails.SingleOrDefault(d => d.Id == id);

        if (detail != null)
        {
            _orderDetails.Remove(detail);

            CalculateTotalAmount();
        }
    }

    public void CalculateTotalAmount()
    {
        TotalAmount = _orderDetails.Sum(d => d.Quantity * d.Price);
    }
}
