namespace SucceSales.Tests.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NFluent;
    using SucceSales.Controllers;
    using SucceSales.Domain.DomainModel;
    using System;
    using Xunit;


    public class SalesControllerTests
    {
        private readonly SalesController _salesController;

        public SalesControllerTests()
        {
            var salesLogger = new Mock<ILogger<SalesController>>();
            var options = new DbContextOptionsBuilder<SalesContext>()
                .UseInMemoryDatabase("AllSales").Options;

            var salesContext = new SalesContext(options);
            salesContext.AllSales.Add(new Sales{Id = 1, ProductId = 1, ProductName = "Banana", Quantity = 3, Date = DateTime.Now});

            _salesController = new SalesController(salesLogger.Object, salesContext);
        }

        [Fact]
        public void TestPostSale_Returns201()
        {
            var expectedSale = new Sales{Id = 2, ProductId = 1, ProductName = "Banana", Quantity = 4, Date = DateTime.Now};
            
            var result = _salesController.PostSale(expectedSale);

            Check.That(result.Result.Result).IsInstanceOf<CreatedAtActionResult>();
            var actionResult = (CreatedAtActionResult)result.Result.Result;
            Check.That(actionResult.StatusCode).IsEqualTo(201);
            Check.That(actionResult.Value).IsEqualTo(expectedSale).And.IsInstanceOf<Sales>();
        }        
    }
}