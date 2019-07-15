namespace OrderProcessor
{
    internal class PaymentProcessor : IPaymentProcessor
    {
        public bool ChargePayment(string ccNumber, decimal productQuantity)
        {
            return true;
        }
    }
}