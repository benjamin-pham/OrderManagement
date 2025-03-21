using FluentValidation;

namespace OrderManagement.Application.UseCases.Orders.GetOrderById;
internal class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(x => x.Id)
           .Must(x => x > 0);
    }
}
