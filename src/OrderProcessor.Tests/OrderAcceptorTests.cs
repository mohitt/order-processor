using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Moq;
using Remotion.Linq.Utilities;

namespace OrderProcessor.Tests
{
    [TestFixture]
    public class OrderAcceptorTests
    {
        [Test]
        public void NoProduct_InStore_Will_Give_NoProduct_Message()
        {
            var listOfProducts = new List<Product>()
            {
                new Product() {Price = 10, Quantity = 0},
            };
            var queryableProducts = listOfProducts.AsQueryable();

            var mockListProducts = new Mock<DbSet<Product>>();
            mockListProducts.As<IQueryable<Product>>().Setup(p => p.Provider).Returns(queryableProducts.Provider);
            mockListProducts.As<IQueryable<Product>>().Setup(p => p.Expression).Returns(queryableProducts.Expression);
//            mockListProducts.As<IQueryable<Product>>().Setup(p => p.ElementType).Returns(queryableProducts.ElementType);
//            mockListProducts.As<IQueryable<Product>>().Setup(p => p.GetEnumerator()).Returns(queryableProducts.GetEnumerator());

            var mockDbContext = new Mock<IDBContext>();
            mockDbContext.Setup(k => k.Products)
                .Returns(mockListProducts.Object);
            var mockCcValidator = new Mock<ICreditCardValidator>();
            var mockPaymentProcessor = new Mock<IPaymentProcessor>();
            var mockEmailSender = new Mock<IEmailSender>();
            var mockMessageWriter = new Mock<IMessageWriter>();
            mockMessageWriter.Setup(m => m.WriteLine(It.Is<string>(k => k == " No product in inventory ")));
            var mockIInputReader = new Mock<IInputReader>();
            var orderAcceptor = new OrderAcceptor(mockDbContext.Object,
                mockCcValidator.Object,
                mockPaymentProcessor.Object,
                mockEmailSender.Object,
                mockMessageWriter.Object,
                mockIInputReader.Object
            );

            orderAcceptor.Accept();
            mockDbContext.VerifyAll();
            mockListProducts.VerifyAll();
            mockMessageWriter.VerifyAll();
        }

        [Test]
        public void Product_InStore_Name_Quantity_Will_Be_Asked()
        {
            var price = 10;
            var listOfProducts = new List<Product>()
            {
                new Product() {Price = price, Quantity = 10},
            };
            var orderQuantity = "2";
            var queryableProducts = listOfProducts.AsQueryable();

            var mockListProducts = new Mock<DbSet<Product>>();
            mockListProducts.As<IQueryable<Product>>().Setup(p => p.Provider).Returns(queryableProducts.Provider);
            mockListProducts.As<IQueryable<Product>>().Setup(p => p.Expression).Returns(queryableProducts.Expression);
//            mockListProducts.As<IQueryable<Product>>().Setup(p => p.ElementType).Returns(queryableProducts.ElementType);
//            mockListProducts.As<IQueryable<Product>>().Setup(p => p.GetEnumerator()).Returns(queryableProducts.GetEnumerator());

            var mockDbContext = new Mock<IDBContext>();
            mockDbContext.Setup(k => k.Products)
                .Returns(mockListProducts.Object);
            var ccNumber = "1232131231";
            var mockCcValidator = new Mock<ICreditCardValidator>();
            mockCcValidator.Setup(m => m.IsValid(It.Is<string>(k => k == ccNumber))).Returns(true);
            var mockPaymentProcessor = new Mock<IPaymentProcessor>();
            mockPaymentProcessor.Setup(m => m.ChargePayment(It.Is<string>(k => k == ccNumber),
                It.Is<decimal>(k => k == int.Parse(orderQuantity) * price)));
            var mockEmailSender = new Mock<IEmailSender>();
            var mockMessageWriter = new Mock<IMessageWriter>();
            mockMessageWriter.Setup(m => m.WriteLine(It.Is<string>(k => k == "Name : ")));
            mockMessageWriter.Setup(m => m.WriteLine(It.Is<string>(k => k == "Quantity : ")));
            mockMessageWriter.Setup(m => m.WriteLine(It.Is<string>(k => k == "Credit Card Number : ")));
            var mockIInputReader = new Mock<IInputReader>();
            mockIInputReader.SetupSequence(k => k.ReadLine())
                .Returns("Test Customer Name")
                .Returns(orderQuantity)
                .Returns(ccNumber);

            var orderAcceptor = new OrderAcceptor(mockDbContext.Object,
                mockCcValidator.Object,
                mockPaymentProcessor.Object,
                mockEmailSender.Object,
                mockMessageWriter.Object,
                mockIInputReader.Object
            );

            orderAcceptor.Accept();
            mockDbContext.VerifyAll();
            mockListProducts.VerifyAll();
            mockMessageWriter.VerifyAll();
            mockCcValidator.VerifyAll();
            mockPaymentProcessor.VerifyAll();
        }
    }
}