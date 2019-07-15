using System;
using System.Linq;

namespace OrderProcessor
{
    public class OrderAcceptor : IOrderAcceptor
    {
        private readonly IDBContext _dbContext;
        private readonly ICreditCardValidator _creditCardValidator;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IEmailSender _emailSender;
        private readonly IMessageWriter _messageWriter;
        private readonly IInputReader _inputReader;

        public OrderAcceptor(IDBContext dbContext, ICreditCardValidator creditCardValidator,
            IPaymentProcessor paymentProcessor, IEmailSender emailSender, IMessageWriter messageWriter,
            IInputReader inputReader)
        {
            _dbContext = dbContext;
            _creditCardValidator = creditCardValidator;
            _paymentProcessor = paymentProcessor;
            _emailSender = emailSender;
            _messageWriter = messageWriter;
            _inputReader = inputReader;
        }

        public void Accept()
        {
            var product = _dbContext.Products.FirstOrDefault();
            if (product == null || product.Quantity == 0)
            {
                _messageWriter.WriteLine(" No product in inventory ");
                return;
            }

            _messageWriter.WriteLine("Name : ");
            var name = _inputReader.ReadLine();

            _messageWriter.WriteLine("Quantity : ");
            var validQuantity = int.TryParse(_inputReader.ReadLine(), out var intQuantity);

            if (!validQuantity)
            {
                _messageWriter.WriteLine("Invalid quantity, please try again");
                return;
            }


            if (product.Quantity < intQuantity)
            {
                _messageWriter.WriteLine("Please try again with less quantity ");
                return;
            }


            _messageWriter.WriteLine("Credit Card Number : ");
            var ccNumber = _inputReader.ReadLine();
            if (!_creditCardValidator.IsValid(ccNumber))
            {
                _messageWriter.WriteLine("Invalid Credit Card number, Please try again ");
                return;
            }

            if (!_paymentProcessor.ChargePayment(ccNumber, intQuantity * product.Price))
            {
                _messageWriter.WriteLine("Could not charge your card, please try again ");
                return;
            }

            _dbContext.Orders.Add(new Order() {Name = name, Quantity = intQuantity, CCNumber = ccNumber});
            product.Quantity -= intQuantity;
            _dbContext.Products.Update(product);

            _dbContext.SaveChanges();

            _emailSender.Send("shipping@freshsoftware.com",
                $"An order has been placed for {product.Name} for {intQuantity}. Please ship that to {name} ");

            _messageWriter.WriteLine(
                $" Thanks you {name}, Your order of {product.Name}, quantity {intQuantity} is submitted successfully ");
        }
    }
}