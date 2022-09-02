namespace SucceSales.Tests.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NFluent;
    using SucceSales.Domain.Entities;
    using SucceSales.Domain.Services;
    using SucceSales.Presentation.Controllers;    
    using SucceSales.Presentation.DTOs;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class SalesControllerTests
    {
        private readonly SalesController _salesController;
        private readonly Mock<ISaleDomainService> _saleDomainService = new();

        public SalesControllerTests()
        {
            var salesLogger = new Mock<ILogger<SalesController>>();
            _salesController = new SalesController(salesLogger.Object, _saleDomainService.Object);
        }

        [Fact]
        public void TestPostSale_PostNewSale_ReturnsOk()
        {
            var expectedSale = new SaleDTO(1, "Banana", 4.0m, 5.0m, DateTime.Now);
            
            var result = _salesController.PostSale(expectedSale);

            Check.That(result.Result).IsInstanceOf<OkResult>();
        }

        [Fact]
        public void TestGetSale_SearchForFirstSale_ReturnsFirstSale()
        {
            var actualDate = DateTime.Now;
            _saleDomainService
                .Setup(x => x.GetSaleById(It.IsAny<int>()))
                .Returns(new SaleMessage(1, "Banana", 4.0m, 5.0m, actualDate));

            var actionResult = _salesController.GetSale(1);

            Check.That(actionResult).IsNotEqualTo(null);
            var result = actionResult.Result as OkObjectResult;
            Check.That(result).IsNotEqualTo(null);
            Check.That(result.Value).IsEqualTo(new SaleDTO(1, "Banana", 4.0m, 5.0m, actualDate));
        }

        [Fact]
        public void TestGetSale_SearchForNonExistingSale_ReturnsNotFound404()
        {
            var actionResult = _salesController.GetSale(-1);

            Check.That(actionResult).IsNotEqualTo(null);
            Check.That(actionResult.Result).IsInstanceOf<NotFoundResult>();
        }
    }
}