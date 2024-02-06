using CrazyOrders.API.OrderProcessors;
using CrazyOrders.Application.Contracts.Messaging;
using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Infrastructure.Messaging;
using CrazyOrders.Infrastructure.PaymentGateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var loggerBuilder = builder.Logging.AddConsole();

builder.Services
    .AddSingleton<IEventBroker, EventBroker>()
    .AddScoped<IPaymentGateway, PaymentGateway>();

builder.Services.AddHostedService<LongRunningOrderProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
