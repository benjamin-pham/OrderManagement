using FluentValidation;

namespace OrderManagement.Application.UseCases.Orders.GetOrderDetailByOrderId;
internal class GetOrderDetailsByOrderIdCommandValidator : AbstractValidator<GetOrderDetailsByOrderIdCommand>
{
    public GetOrderDetailsByOrderIdCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .Must(x => x > 0);
    }
}
