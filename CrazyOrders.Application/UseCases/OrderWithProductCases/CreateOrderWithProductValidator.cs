using FluentValidation;

namespace CrazyOrders.Application.UseCases.OrderWithProductCases
{
    public class CreateOrderWithProductValidator : AbstractValidator<CreateOrderWithProductCommand>
    {
        public CreateOrderWithProductValidator()
        {
            RuleFor(x => x.Order).NotNull();
            RuleFor(x => x.Order.PaymentDetail).NotNull();
            RuleFor(x => x.Order.Products).NotEmpty();
        }
    }
}
