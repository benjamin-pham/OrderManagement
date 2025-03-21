using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Shared;
using OrderManagement.Application.UseCases.Orders.DeleteOrder;
using OrderManagement.Application.UseCases.Orders.RemoveOrderDetail;
using OrderManagement.Domain.Abstractions;

namespace OrderManagement.API.Controllers;
[Route("api/order-details")]
[ApiController]
public class OrderDetailsController : ControllerBase
{
    private readonly ISender _sender;

    public OrderDetailsController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// remove order detail
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteOrderDetails(int id)
    {
        RemoveOrderDetailCommand command = new RemoveOrderDetailCommand(id);

        Result result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}
