using CrazyOrders.Application.UseCases.OrderWithServiceCases;
using FluentValidation;

namespace CrazyOrders.Application.UseCases.OrderWithProductCases
{
    public class CreateOrderWithServiceValidator : AbstractValidator<CreateOrderWithServiceCommand>
    {
        public CreateOrderWithServiceValidator()
        {
            RuleFor(x => x.Order).NotNull();
            RuleFor(x => x.Order.PaymentDetail).NotNull();
            RuleFor(x => x.Order.Services).NotEmpty();
        }
    }
}
