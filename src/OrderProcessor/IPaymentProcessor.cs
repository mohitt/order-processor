namespace OrderProcessor
{
    public interface IPaymentProcessor
    {
        bool ChargePayment(string ccNumber, decimal productQuantity);
    }
}