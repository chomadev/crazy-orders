using CrazyOrders.Application.Contracts.Deliverables;
using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Domain.Exceptions;
using CrazyOrders.Domain.ValueObjects.Orders;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Application.UseCases.OrderWithProductCases
{
    public record CreateOrderWithProductCommand(
        OrderWithProduct Order) : IRequest;

    public class CreateOrderWithProductCommandHandler : IRequestHandler<CreateOrderWithProductCommand>
    {
        private readonly ILogger<CreateOrderWithProductCommandHandler> logger;
        private readonly IProductShipping productShipping;
        private readonly IPaymentGateway paymentGateway;

        public CreateOrderWithProductCommandHandler(ILogger<CreateOrderWithProductCommandHandler> logger, IProductShipping productShipping, IPaymentGateway paymentGateway)
        {
            this.logger = logger;
            this.productShipping = productShipping;
            this.paymentGateway = paymentGateway;
        }

        public async Task Handle(CreateOrderWithProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create Order With Product - order created {1}", request.Order.Id);

            var transactionResult = await paymentGateway.ProcessTransaction(request.Order.PaymentDetail);
            if (transactionResult != TransactionStatus.Ok)
            {
                throw new DomainException($"Error processing transaction: {transactionResult}");
            }

            logger.LogInformation("Create Order With Product - initiating delivery for order {1}", request.Order.Id);
            foreach (var product in request.Order.Products)
            {
                productShipping.Ship(product);
            }
        }
    }
}
