// See https://aka.ms/new-console-template for more information
using CrazyOrders.Application.Processors;
using CrazyOrders.Domain.ValueObjects;
using CrazyOrders.Infrastructure.Messaging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});

var logger = loggerFactory.CreateLogger<EventBroker>();

logger.LogInformation("OrderProcessor started {1}", Thread.CurrentThread.Name);
