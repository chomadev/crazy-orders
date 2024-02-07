using CrazyOrders.Application.Contracts.Deliverables;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Infrastructure.Deliverables
{
    public class ServiceActivator : IServiceActivator
    {
        private readonly ILogger<ServiceActivator> logger;

        public ServiceActivator(ILogger<ServiceActivator> logger)
        {
            this.logger = logger;
        }

        public void Activate(string service)
        {
            logger.LogInformation("Service Activator - activating services {service}", service);
        }
    }
}
