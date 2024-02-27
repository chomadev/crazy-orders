using CrazyOrders.Application.UseCases.OrderWithProductCases;
using CrazyOrders.Application.UseCases.OrderWithServiceCases;
using CrazyOrders.Domain.Entities.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CrazyOrders.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly IMediator mediator;

        public OrderController(ILogger<OrderController> logger,
            IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpPost("/product", Name = "Create Order with Product")]
        public async Task<IActionResult> CreateOrderWithProduct(OrderWithProduct order)
        {
            logger.LogInformation("Create order requested: {id}", order.Id);
            await mediator.Send(new CreateOrderWithProductCommand(order));
            return Created();
        }

        [HttpPost("/service", Name = "Create Order with Service")]
        public async Task<IActionResult> CreateOrderWithService(OrderWithService order)
        {
            logger.LogInformation("Create order requested: {id}", order.Id);
            await mediator.Send(new CreateOrderWithServiceCommand(order));
            return Created();
        }
    }
}
