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
    using System.Threading.Tasks;
    using Xunit;

    public class SalesControllerTests
    {
        private readonly SalesController _salesController;
        private readonly Sales _firstSale = new Sales{Id = 1, ProductId = 1, ProductName = "Banana", Quantity = 3, Date = DateTime.Now};

        public SalesControllerTests()
        {
            var salesLogger = new Mock<ILogger<SalesController>>();
            var options = new DbContextOptionsBuilder<SalesContext>()
                .UseInMemoryDatabase("AllSales").Options;

            var salesContext = new SalesContext(options);
            salesContext.AllSales.Add(_firstSale);

            _salesController = new SalesController(salesLogger.Object, salesContext);
        }

        [Fact]
        public void TestPostSale_PostNewSale_ReturnsCreated201()
        {
            var expectedSale = new Sales{Id = 2, ProductId = 1, ProductName = "Banana", Quantity = 4, Date = DateTime.Now};
            
            var result = _salesController.PostSale(expectedSale);

            Check.That(result.Result.Result).IsInstanceOf<CreatedAtActionResult>();
            var actionResult = (CreatedAtActionResult)result.Result.Result;
            Check.That(actionResult.StatusCode).IsEqualTo(201);
            Check.That(actionResult.Value).IsEqualTo(expectedSale).And.IsInstanceOf<Sales>();
        }

        [Fact]
        public void TestPostSale_PostExistingSale_ReturnsException()
        {
            var expectedSale = new Sales{Id = 1, ProductId = 1, ProductName = "Banana", Quantity = 4, Date = DateTime.Now};
            
            var result = _salesController.PostSale(expectedSale);

            Check.That(result.Status).IsEqualTo(TaskStatus.Faulted);
            Check.That(result.Exception.HResult).IsEqualTo(-2146233088);
        }

        [Fact]
        public void TestGetSale_SearchForFirstSale_ReturnsFirstSale()
        {
            var result = _salesController.GetSale(1);

            Check.That(result.Result).IsInstanceOf<ActionResult<Sales>>();
            var actionResult = (ActionResult<Sales>)result.Result;
            Check.That(actionResult.Value).IsEqualTo(_firstSale).And.IsInstanceOf<Sales>();
        }

        [Fact]
        public void TestGetSale_SearchForNonExistingSale_ReturnsNotFound404()
        {
            var result = _salesController.GetSale(2);

            Check.That(result.Result).IsInstanceOf<ActionResult<Sales>>();
            var actionResult = (ActionResult<Sales>)result.Result;
            Check.That(actionResult.Value).IsEqualTo(null);

            Check.That(actionResult.Result).IsInstanceOf<NotFoundResult>();
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Check.That(notFoundResult.StatusCode).IsEqualTo(404);
        }
    }
}