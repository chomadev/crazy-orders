using CrazyOrders.Application.Contracts.PaymentGateway;
using CrazyOrders.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace CrazyOrders.Infrastructure.PaymentGateway
{
    public class PaymentGateway : IPaymentGateway
    {
        private readonly ILogger<PaymentGateway> logger;

        public PaymentGateway(ILogger<PaymentGateway> logger)
        {
            this.logger = logger;
        }

        public Task<TransactionStatus> ProcessTransaction(PaymentDetail paymentDetails)
        {
            if (paymentDetails == null)
            {
                throw new ArgumentNullException(nameof(paymentDetails));
            }
            logger.LogInformation("Processing transaction");
            return Task.FromResult(TransactionStatus.Ok);
        }
    }
}
