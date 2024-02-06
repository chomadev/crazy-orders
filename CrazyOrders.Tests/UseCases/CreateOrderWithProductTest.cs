using CrazyOrders.Application.Contracts.Messaging;
using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Application.UseCases;
using CrazyOrders.Domain.Events;
using CrazyOrders.Domain.Exceptions;
using CrazyOrders.Domain.ValueObjects;
using CrazyOrders.Domain.ValueObjects.Orders;
using Moq;

namespace CrazyOrders.Tests.UseCases
{
    public class CreateOrderWithProductTest
    {
        private readonly Mock<IEventBroker> eventBroker;
        private readonly Mock<IPaymentGateway> paymentGatewayMock;
        private CreateOrderWithProduct? sut;

        private OrderWithProduct orderWithPayment = new OrderWithProduct()
        {
            PaymentDetail = new PaymentDetail(),
            Products = ["Product 1"]
        };

        private OrderWithProduct orderWithInvalidProducts = new OrderWithProduct()
        {
            PaymentDetail = new PaymentDetail(),
            Products = []
        };

        private OrderWithProduct orderWithInvalidPayment = new OrderWithProduct()
        {
            PaymentDetail = null,
            Products = ["Produt 1"]
        };

        public CreateOrderWithProductTest()
        {
            eventBroker = new Mock<IEventBroker>();
            paymentGatewayMock = new Mock<IPaymentGateway>();
        }

        [Fact]
        public async Task CreateOrderWithProduct_ShouldEmit_OrderCreated_WithValidPayload()
        {
            sut = new CreateOrderWithProduct(eventBroker.Object,
                paymentGatewayMock.Object,
                orderWithPayment);

            await sut.Run();
            eventBroker.Verify(e => e.Push(It.IsAny<OrderCreated>()));
        }

        [Fact]
        public async Task CreateOrderWithProduct_ShouldEmit_NoEvent_WithInvalidPayload()
        {
            await Assert.ThrowsAsync<DomainException>(async () =>
            {
                sut = new CreateOrderWithProduct(eventBroker.Object,
                    paymentGatewayMock.Object,
                    orderWithInvalidProducts);

                await sut.Run();
            });
            eventBroker.Verify(e => e.Push(It.IsAny<OrderCreated>()), Times.Never);
        }

        [Fact]
        public async Task CreateOrderWithProduct_ShouldEmit_NoEvent_WithInvalidPaymentDetails()
        {
            await Assert.ThrowsAsync<DomainException>(async () =>
            {
                sut = new CreateOrderWithProduct(eventBroker.Object,
                    paymentGatewayMock.Object,
                    orderWithInvalidPayment);

                await sut.Run();
            });

            eventBroker.Verify(e => e.Push(It.IsAny<OrderCreated>()), Times.Never);
        }
    }
}
