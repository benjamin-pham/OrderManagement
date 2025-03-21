using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OrderManagement.Application.Shared;
using OrderManagement.Application.UseCases.Orders.CreateOrder;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Application.UnitTests.Orders;
public class CreateOrderTests
{
    private static readonly CreateOrderCommand Command = new(OrderData.CustomerName);
    private readonly CreateOrderCommandHandler _handler;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateOrderTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();

        _handler = new CreateOrderCommandHandler(_orderRepository,
            _unitOfWork,
            _mapper);
    }

    [Fact]
    public async Task OrderRepository_ShouldCallOrderRepository_WhenOrderCreated()
    {
        // Arrange

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        _orderRepository.Received(1).Create(Arg.Is<Order>(o => o.CustomerName == Command.CustomerName));
    }

    [Fact]
    public async Task UnitOfWork_ShouldCallSaveChange_WhenOrderCreated()
    {
        // Arrange

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        _orderRepository.Received(1).Create(Arg.Is<Order>(o => o.CustomerName == Command.CustomerName));
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenOrderCreated()
    {
        // Arrange
        var order = OrderData.Create();

        var response = new OrderResponse
        {
            Id = 1,
            CustomerName = Command.CustomerName,
            Status = OrderStatus.Pending,
            TotalAmount = 0,
        };

        _mapper.Map<OrderResponse>(order).Returns(response);

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        _orderRepository.Received(1).Create(Arg.Is<Order>(o => o.CustomerName == Command.CustomerName));
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(Arg.Is<OrderResponse>(o => o.CustomerName == response.CustomerName
            && o.Id == response.Id
            && o.Status == response.Status
            && o.TotalAmount == response.TotalAmount));
    }
}
