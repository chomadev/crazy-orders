using CrazyOrders.Application.Contracts;
using CrazyOrders.Application.Contracts.Messaging;
using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Domain.Events;
using CrazyOrders.Domain.Exceptions;
using CrazyOrders.Domain.ValueObjects.Orders;
using Newtonsoft.Json;

namespace CrazyOrders.Application.UseCases
{
    public class CreateOrderWithServiceActivation : IUseCase
    {
        private readonly IEventBroker eventBroker;
        private readonly IPaymentGateway paymentGateway;
        private readonly OrderWithService orderWithService;

        public CreateOrderWithServiceActivation(
            IEventBroker eventBroker,
            IPaymentGateway paymentGateway,
            OrderWithService orderWithService)
        {
            this.eventBroker = eventBroker;
            this.orderWithService = orderWithService;
            this.paymentGateway = paymentGateway;
        }

        public async Task Run()
        {
            if (!orderWithService.IsValid())
            {
                throw new DomainException("Order is invalid");
            }

            if (orderWithService.PaymentDetail == null)
            {
                throw new DomainException("Payment Detail is required");
            }

            var transactionResult = await paymentGateway.ProcessTransaction(orderWithService.PaymentDetail);

            if (transactionResult == TransactionStatus.Ok)
            {
                var eventPayload = JsonConvert.SerializeObject(orderWithService);
                eventBroker.Push(new OrderCreated(eventPayload));

                eventBroker.Push(new OrderCreated(eventPayload));
            }
            else
            {
                throw new DomainException($"Unable to process transaction for order {orderWithService.Id}, result: {transactionResult}");
            }
        }
    }
}
/*
 todo:
    - entrega de serviço
        trocar estrutura do processador de eventos para Queue
        usar processador de eventos para pegar tipo do evento
        criar sub-processador de evento por tipo de evento
        transformar sub-processador de OrderWithProduct para ter entrega
        criar sub-processador de OrderWithService para ter ativação
 */