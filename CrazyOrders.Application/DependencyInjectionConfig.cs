using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using CrazyOrders.Application.UseCases.OrderWithProductCases;
using CrazyOrders.Application.UseCases.OrderWithServiceCases;

namespace CrazyOrders.Application
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IValidator<CreateOrderWithProductCommand>, CreateOrderWithProductValidator>();
            services.AddScoped<IValidator<CreateOrderWithServiceCommand>, CreateOrderWithServiceValidator>();
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }
    }
}
