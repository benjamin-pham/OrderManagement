using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.DTOs;
using OrderManagement.Application.Shared;
using OrderManagement.Application.UseCases.Orders.AddOrderDetail;
using OrderManagement.Application.UseCases.Orders.CreateOrder;
using OrderManagement.Application.UseCases.Orders.DeleteOrder;
using OrderManagement.Application.UseCases.Orders.GetOrderById;
using OrderManagement.Application.UseCases.Orders.GetOrderDetailByOrderId;
using OrderManagement.Application.UseCases.Orders.GetOrders;
using OrderManagement.Application.UseCases.Orders.UpdateOrder;
using OrderManagement.Domain.Abstractions;
using OrderManagement.Domain.Orders;

namespace OrderManagement.API.Controllers;
[Route("api/orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// create order
    /// </summary>
    /// <param name="request"></param>
    /// <returns>order model</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequest request)
    {
        CreateOrderCommand command = new CreateOrderCommand(request.CustomerName!);

        Result<OrderResponse> result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// update order
    /// </summary>
    /// <param name="id">order id</param>
    /// <param name="request"></param>
    /// <returns>order updated</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Result<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequest request)
    {
        UpdateOrderCommand command = new UpdateOrderCommand(id, request.CustomerName!, (OrderStatus)request.Status!.Value);

        Result<OrderResponse> result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// remove order
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>order updated</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand(id);

        Result result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// get order by id
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int id)
    {
        GetOrderByIdQuery query = new GetOrderByIdQuery(id);

        Result<OrderResponse> result = await _sender.Send(query);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// get order pagination
    /// </summary>
    /// <param name="getOrdersQuery"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<PagedList<OrderResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOrderPagination([FromQuery] GetOrdersQuery getOrdersQuery)
    {
        Result<PagedList<OrderResponse>> result = await _sender.Send(getOrdersQuery);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// add order detail
    /// </summary>
    /// <param name="id">order id</param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{id}/order-details")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddOrderDetail(int id, [FromBody] AddOrderDetailRequest request)
    {
        AddOrderDetailCommand command = new AddOrderDetailCommand(
            id,
            request.ProductName!,
            request.Quantity!.Value,
            request.Price!.Value);

        Result result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
    /// <summary>
    /// get order detail by order id
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns></returns>
    [HttpGet("{id}/order-details")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<IEnumerable<OrderDetailResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOrderDetails(int id)
    {
        GetOrderDetailsByOrderIdCommand command = new GetOrderDetailsByOrderIdCommand(id);

        Result<IEnumerable<OrderDetailResponse>> result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }

}
