using System;
using FluentAssertions;
using OrderManagement.Domain.Exceptions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Domain.UnitTests.Orders;

public class OrderDetailTests
{
    [Fact]
    public void Create_ShouldSetPropertyValues_WhenValidInput()
    {
        // Arrange

        // Act
        var orderDetail = OrderDetail.Create(OrderData.OrderId, OrderData.ProductName, OrderData.Quantity, OrderData.Price);

        // Assert
        orderDetail.Should().NotBeNull();
        orderDetail.OrderId.Should().Be(OrderData.OrderId);
        orderDetail.ProductName.Should().Be(OrderData.ProductName);
        orderDetail.Quantity.Should().Be(OrderData.Quantity);
        orderDetail.Price.Should().Be(OrderData.Price);
    }

    [Fact]
    public void Create_ShouldThrowException_WhenProductNameIsNull()
    {
        // Arrange

        // Act
        Action result = () => OrderDetail.Create(OrderData.OrderId, null, OrderData.Quantity, OrderData.Price);

        // Assert
        result.Should().Throw<AppException>().WithMessage("ProductName cannot be null or empty");
    }

    [Fact]
    public void Create_ShouldThrowException_WhenQuantityLessThanOne()
    {
        // Arrange

        // Act
        Action result = () => OrderDetail.Create(OrderData.OrderId, OrderData.ProductName, 0, OrderData.Price);

        // Assert
        result.Should().Throw<AppException>().WithMessage("Quantity must be greater than zero");
    }

    [Fact]
    public void Create_ShouldThrowException_WhenPriceLessThanOne()
    {
        // Arrange

        // Act
        Action result = () => OrderDetail.Create(OrderData.OrderId, OrderData.ProductName, OrderData.Quantity, 0);

        // Assert
        result.Should().Throw<AppException>().WithMessage("Price must be greater than zero");
    }
}
