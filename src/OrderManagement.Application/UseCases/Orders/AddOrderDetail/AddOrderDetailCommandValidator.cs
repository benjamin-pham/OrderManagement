using FluentValidation;

namespace OrderManagement.Application.UseCases.Orders.AddOrderDetail;
internal class AddOrderDetailCommandValidator : AbstractValidator<AddOrderDetailCommand>
{
    public AddOrderDetailCommandValidator()
    {
        RuleFor(x => x.orderId)
           .Must(x => x > 0);

        RuleFor(x => x.ProductName)
            .NotNull()
            .Length(1, 255);

        RuleFor(x => x.Quantity)
            .Must(x => x > 0);

        RuleFor(x => x.Price)
            .Must(x => x > 0);
    }
}
