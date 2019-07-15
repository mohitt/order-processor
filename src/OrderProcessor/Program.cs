using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OrderProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var orderAcceptor = serviceProvider.GetService<IOrderAcceptor>();
            orderAcceptor.Accept();
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IOrderAcceptor, OrderAcceptor>()
                .AddSingleton<IMessageWriter, MessageWriter>()
                .AddSingleton<IInputReader, InputReader>()
                .AddSingleton<IEmailSender, EmailSender>()
                .AddSingleton<IDBContext, OrderDBContext>()
                .AddSingleton<IPaymentProcessor, PaymentProcessor>()
                .AddSingleton<ICreditCardValidator, CreditCardValidator>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}