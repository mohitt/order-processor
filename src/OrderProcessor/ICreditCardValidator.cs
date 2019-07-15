namespace OrderProcessor
{
    public interface ICreditCardValidator
    {
        bool IsValid(string ccNumber);
    }
}