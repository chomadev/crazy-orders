
using CrazyOrders.Domain.ValueObjects;

namespace CrazyOrders.Application.Contracts.PaymentGateway
{
    public interface IPaymentGateway
    {
        Task<TransactionStatus> ProcessTransaction(PaymentDetail paymentDetails);
    }

    public enum TransactionStatus
    {
        Ok,
        Error,
        InsuficientFunds
    }
}
