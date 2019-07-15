namespace OrderProcessor
{
    internal class CreditCardValidator : ICreditCardValidator
    {
        public bool IsValid(string ccNumber)
        {
            return true;
        }
    }
}