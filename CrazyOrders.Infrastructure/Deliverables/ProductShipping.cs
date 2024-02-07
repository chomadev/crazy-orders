using CrazyOrders.Application.Contracts.Deliverables;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Infrastructure.Deliverables
{
    public class ProductShipping : IProductShipping
    {
        private readonly ILogger<ProductShipping> logger;

        public ProductShipping(ILogger<ProductShipping> logger)
        {
            this.logger = logger;
        }

        public void Ship(string product)
        {
            logger.LogInformation("Product Shipping - shipping product {product}", product);
        }
    }
}
