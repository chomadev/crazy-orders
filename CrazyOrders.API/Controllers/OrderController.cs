using CrazyOrders.Application.Contracts.Messaging;
using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Application.UseCases;
using CrazyOrders.Domain.ValueObjects.Orders;
using Microsoft.AspNetCore.Mvc;

namespace CrazyOrders.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly IEventBroker eventBroker;
        private readonly IPaymentGateway paymentGateway;

        public OrderController(ILogger<OrderController> logger,
            IEventBroker eventBroker,
            IPaymentGateway paymentGateway)
        {
            this.logger = logger;
            this.eventBroker = eventBroker;
            this.paymentGateway = paymentGateway;
        }

        [HttpPost("/product", Name = "Create Order with Product")]
        public async Task<IActionResult> CreateOrderWithProduct(OrderWithProduct order)
        {
            logger.LogInformation("Create order requested: {id}", order.Id);
            if (!order.IsValid())
            {
                return BadRequest();
            }
            await new CreateOrderWithProduct(eventBroker, paymentGateway, order)
                .Run();
            return Created();
        }

        [HttpPost("/service", Name = "Create Order with Service")]
        public async Task<IActionResult> CreateOrderWithService(OrderWithService order)
        {
            logger.LogInformation("Create order requested: {id}", order.Id);
            if (!order.IsValid())
            {
                return BadRequest();
            }
            await new CreateOrderWithServiceActivation(eventBroker, paymentGateway, order)
                .Run();
            return Created();
        }
    }
}
