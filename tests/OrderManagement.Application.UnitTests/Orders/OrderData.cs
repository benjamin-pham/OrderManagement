using System;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UnitTests.Orders;

public class OrderData
{
    public static Order Create() => Order.Create(CustomerName);

    public const string CustomerName = "Phạm Minh Mẫn";
    public const string NewCustomerName = "Benjamin Pham";
    public const string ProductName = "Product 1";
    public const decimal TotalAmount = Price * Quantity;
    public const decimal Price = 1000;
    public const int Quantity = 2;
    public const OrderStatus NewStatus = OrderStatus.Completed;
    public const int OrderId = 1;
}
