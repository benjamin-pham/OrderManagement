using FluentAssertions;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Domain.UnitTests.Orders;
public class OrderTests
{
    [Fact]
    public void Create_ShouldSetPropertyValues_WhenValidInput()
    {
        // Arrange

        // Act
        var order = Order.Create(OrderData.CustomerName);

        // Assert
        order.Should().NotBeNull();
        order.CustomerName.Should().Be(OrderData.CustomerName);
        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenInvalidInput()
    {
        // Arrange
        string customerName = null;

        // Act
        Action result = () => Order.Create(customerName);

        // Assert
        result.Should().Throw<AppException>().WithMessage("CustomerName cannot be null or empty");
    }

    [Fact]
    public void Update_Should_SetPropertyValues()
    {
        // Arrange
        var order = OrderData.Create();

        // Act
        order.Update(OrderData.NewCustomerName, OrderData.NewStatus);

        // Assert
        order.CustomerName.Should().Be(OrderData.NewCustomerName);
        order.Status.Should().Be(OrderData.NewStatus);
    }

    [Fact]
    public void Update_ShouldThrowException_WhenInvalidInput()
    {
        // Arrange
        var order = OrderData.Create();

        // Act
        Action result = () => order.Update(null, OrderStatus.Completed);

        // Assert
        result.Should().Throw<AppException>().WithMessage("CustomerName cannot be null or empty");
    }

    [Fact]
    public void AddDetail_ShouldAddDetailAndUpdateTotalAmount_WhenDetailsAreValids()
    {
        // Arrange
        var order = OrderData.Create();

        // Act
        order.AddDetail(OrderData.ProductName, OrderData.Quantity, OrderData.Price);

        // Assert
        order.OrderDetails.Should().HaveCount(1);
        order.TotalAmount.Should().Be(OrderData.TotalAmount);
    }

    [Fact]
    public void RemoveDetail_ShouldRemoveDetailAndUpdateTotalAmount_WhenDetailsAreValids()
    {
        // Arrange
        var order = OrderData.Create();
        order.AddDetail(OrderData.ProductName, OrderData.Quantity, OrderData.Price);
        var detailId = order.OrderDetails.First().Id;

        // Act
        order.RemoveDetail(detailId);

        // Assert
        order.OrderDetails.Should().BeEmpty();
        order.TotalAmount.Should().Be(0);
    }
}
