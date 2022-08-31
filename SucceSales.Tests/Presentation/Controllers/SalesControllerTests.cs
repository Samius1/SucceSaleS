namespace SucceSales.Tests.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NFluent;
    using SucceSales.Storage;
    using SucceSales.Storage.Entities;
    using SucceSales.Presentation.Controllers;    
    using SucceSales.Presentation.DTOs;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class SalesControllerTests
    {
        private readonly SalesController _salesController;
        private readonly Sales _firstSale = new Sales(0, 1, "Banana", 3m, 5m, DateTime.Now, 15m);

        public SalesControllerTests()
        {
            var salesLogger = new Mock<ILogger<SalesController>>();
            var options = new DbContextOptionsBuilder<SucceSalesContext>()
                .UseInMemoryDatabase("AllSales").Options;

            var salesContext = new SucceSalesContext(options);
            salesContext.Sales.Add(_firstSale);

            _salesController = new SalesController(salesLogger.Object, salesContext);
        }

        [Fact]
        public void TestPostSale_PostNewSale_ReturnsCreated201()
        {
            var expectedSale = new SalesDTO(1, "Banana", 4.0m, 5.0m, DateTime.Now, 20.0m);
            
            var result = _salesController.PostSale(expectedSale);

            Check.That(result.Result.Result).IsInstanceOf<CreatedAtActionResult>();
            var actionResult = (CreatedAtActionResult)result.Result.Result;
            Check.That(actionResult.StatusCode).IsEqualTo(201);
            Check.That(actionResult.Value).IsEqualTo(expectedSale).And.IsInstanceOf<SalesDTO>();
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
            var result = _salesController.GetSale(-1);

            Check.That(result.Result).IsInstanceOf<ActionResult<Sales>>();
            var actionResult = (ActionResult<Sales>)result.Result;
            Check.That(actionResult.Value).IsEqualTo(null);

            Check.That(actionResult.Result).IsInstanceOf<NotFoundResult>();
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Check.That(notFoundResult.StatusCode).IsEqualTo(404);
        }
    }
}