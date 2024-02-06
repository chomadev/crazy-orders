using CrazyOrders.Application.Contracts;
using CrazyOrders.Application.Contracts.Messaging;
using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Domain.Events;
using CrazyOrders.Domain.Exceptions;
using CrazyOrders.Domain.ValueObjects.Orders;
using Newtonsoft.Json;

namespace CrazyOrders.Application.UseCases
{
    public class CreateOrderWithProduct : IUseCase
    {
        private readonly IEventBroker eventBroker;
        private readonly IPaymentGateway paymentGateway;
        private readonly OrderWithProduct order;

        public CreateOrderWithProduct(IEventBroker eventBroker, IPaymentGateway paymentGateway, OrderWithProduct order)
        {
            this.eventBroker = eventBroker;
            this.order = order;
            this.paymentGateway = paymentGateway;
        }

        public async Task Run()
        {
            if (!order.IsValid())
            {
                throw new DomainException("Order is invalid");
            }

            if (order.PaymentDetail == null)
            {
                throw new DomainException("Payment Detail is required");
            }

            var transactionResult = await paymentGateway.ProcessTransaction(order.PaymentDetail);

            if (transactionResult == TransactionStatus.Ok)
            {
                var eventPayload = JsonConvert.SerializeObject(order);
                eventBroker.Push(new OrderCreated(eventPayload));
            }
            else
            {
                throw new DomainException($"Unable to process transaction for order {order.Id}, result: {transactionResult}");
            }
        }
    }
}
