using CrazyOrders.Domain.ValueObjects;
using CrazyOrders.Domain.ValueObjects.Orders;

namespace CrazyOrders.Tests.Fakes
{
    internal class OrderWithProductFakes
    {
        internal static OrderWithProduct OrderWithPayment = new OrderWithProduct()
        {
            PaymentDetail = new PaymentDetail(),
            Products = ["Product 1"]
        };

        internal static OrderWithProduct OrderWithInvalidProducts = new OrderWithProduct()
        {
            PaymentDetail = new PaymentDetail(),
            Products = []
        };

        internal static OrderWithProduct OrderWithInvalidPayment = new OrderWithProduct()
        {
            PaymentDetail = null,
            Products = ["Produt 1"]
        };
    }
}
