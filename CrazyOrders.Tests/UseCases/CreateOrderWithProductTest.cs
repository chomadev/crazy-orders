using CrazyOrders.Application.Contracts.Deliverables;
using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Application.UseCases.OrderWithProductCases;
using CrazyOrders.Tests.Fakes;
using Microsoft.Extensions.Logging;
using Moq;

namespace CrazyOrders.Tests.UseCases
{
    public class CreateOrderWithProductTest
    {
        private readonly Mock<IPaymentGateway> paymentGatewayMock;
        private readonly Mock<IProductShipping> productShipping;
        private readonly Mock<ILogger<CreateOrderWithProductCommandHandler>> logger;
        private CreateOrderWithProductCommandHandler? sut;

        public CreateOrderWithProductTest()
        {
            paymentGatewayMock = new Mock<IPaymentGateway>();
            productShipping = new Mock<IProductShipping>();
            logger = new Mock<ILogger<CreateOrderWithProductCommandHandler>>();

            sut = new CreateOrderWithProductCommandHandler(
                logger.Object,
                productShipping.Object,
                paymentGatewayMock.Object);
        }

        [Fact]
        public async Task CreateOrderWithProduct_Should_ProcessTransaction_WithValidPayload()
        {
            var cmd = new CreateOrderWithProductCommand(OrderWithProductFakes.OrderWithPayment);

            await sut.Handle(cmd, default);
            paymentGatewayMock.Verify(e => e.ProcessTransaction(OrderWithProductFakes.OrderWithPayment.PaymentDetail));
        }
    }
}
