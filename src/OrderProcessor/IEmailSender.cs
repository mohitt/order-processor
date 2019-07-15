namespace OrderProcessor
{
    public interface IEmailSender
    {
        void Send(string email, string body);
    }
}