using CrazyOrders.Application.Contracts.Deliverables;
using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Infrastructure.Deliverables;
using CrazyOrders.Infrastructure.PaymentGateway;
using Microsoft.Extensions.DependencyInjection;

namespace CrazyOrders.Application
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentGateway, PaymentGateway>();
            services.AddScoped<IProductShipping, ProductShipping>();
            services.AddScoped<IServiceActivator, ServiceActivator>();
            return services;
        }
    }
}
