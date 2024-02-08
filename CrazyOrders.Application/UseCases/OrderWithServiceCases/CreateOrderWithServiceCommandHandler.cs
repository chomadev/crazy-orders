using CrazyOrders.Application.Contracts.Deliverables;
using CrazyOrders.Domain.ValueObjects.Orders;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Application.UseCases.OrderWithServiceCases
{
    public record CreateOrderWithServiceCommand(OrderWithService Order) : IRequest;

    public class CreateOrderWithServiceCommandHandler : IRequestHandler<CreateOrderWithServiceCommand>
    {
        private readonly ILogger<CreateOrderWithServiceCommandHandler> logger;
        private readonly IServiceActivator serviceActivator;

        public CreateOrderWithServiceCommandHandler(ILogger<CreateOrderWithServiceCommandHandler> logger, IServiceActivator serviceActivator)
        {
            this.logger = logger;
            this.serviceActivator = serviceActivator;
        }

        public Task Handle(CreateOrderWithServiceCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create Order With Service - order created {1}", request.Order.Id);
            logger.LogInformation("Create Order With Service - initiating service activation for order {1}", request.Order.Id);
            foreach (var service in request.Order.Services)
            {
                serviceActivator.Activate(service);
            }

            //emit OrderCreated event

            return Task.CompletedTask;
        }
    }
}
