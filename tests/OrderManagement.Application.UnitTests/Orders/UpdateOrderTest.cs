using System;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrderManagement.Application.Shared;
using OrderManagement.Application.UseCases.Orders.UpdateOrder;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.Application.UnitTests.Orders;

public class UpdateOrderTest
{
    private static readonly UpdateOrderCommand Command = new(OrderData.OrderId, OrderData.NewCustomerName, OrderData.NewStatus);
    private readonly UpdateOrderCommandHandler _handler;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateOrderTest()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();

        _handler = new UpdateOrderCommandHandler(_orderRepository,
            _unitOfWork,
            _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenOrderIsNull()
    {
        // Arrange
        _orderRepository
            .GetByIdAsync(Command.Id, Arg.Any<CancellationToken>())
            .Returns((Order?)null);

        // Act
        Result<OrderResponse> result = await _handler.Handle(Command, default);

        // Assert
        result.Value.Should().BeNull();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_ShouldCallOrderRepository_WhenOrderIsUpdated()
    {
        // Arrange
        var order = OrderData.Create();

        _orderRepository
            .GetByIdAsync(Command.Id, Arg.Any<CancellationToken>())
            .Returns(order);

        // Act
        Result<OrderResponse> result = await _handler.Handle(Command, default);

        // Assert
        _orderRepository.Received(1).Update(Arg.Is<Order>(o => o.CustomerName == Command.CustomerName && o.Status == Command.Status));
    }

    [Fact]
    public async Task UnitOfWork_ShouldCallSaveChange_WhenOrderCreated()
    {
        // Arrange
        var order = OrderData.Create();
        order.Id = 1;
        _orderRepository
            .GetByIdAsync(Command.Id, Arg.Any<CancellationToken>())
            .Returns(order);

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        _orderRepository.Received(1).Update(Arg.Is<Order>(o => o.CustomerName == Command.CustomerName && o.Status == Command.Status));
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_ShouldCreateOrderResponse_WhenOrderCreated()
    {
        // Arrange
        var order = OrderData.Create();

        _orderRepository
            .GetByIdAsync(Command.Id, Arg.Any<CancellationToken>())
            .Returns(order);

        var response = new OrderResponse
        {
            Id = 0,
            CustomerName = Command.CustomerName,
            Status = OrderStatus.Pending,
            TotalAmount = 0,
        };

        _mapper.Map<OrderResponse>(order).Returns(response);

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        _orderRepository.Received(1).Update(Arg.Is<Order>(o => o.CustomerName == Command.CustomerName && o.Status == Command.Status));
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
        _mapper.Received(1).Map<OrderResponse>(Arg.Is<Order>(o => o.CustomerName == Command.CustomerName && o.Status == Command.Status));
    }


}
