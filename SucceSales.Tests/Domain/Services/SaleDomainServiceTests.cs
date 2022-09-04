namespace SucceSales.Tests.Domain.Services
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using NFluent;
    using SucceSales.Domain.Entities;
    using SucceSales.Domain.Services;
    using SucceSales.Storage.Entities;
    using SucceSales.Storage.Repositories;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class SaleDomainServiceTests
    {
        private readonly SaleDomainService _salesDomainService;
        private readonly Mock<ISalesRepository> _salesRepository = new();
        private DateTime actualDate = DateTime.Now;
 
        public SaleDomainServiceTests()
        {
            _salesDomainService = new SaleDomainService(_salesRepository.Object);
        }

        [Fact]
        public void TestGetSaleById_SaleExists_ReturnsSale()
        {
            var expectedSaleMessage = new SaleMessage(1, "Banana", 4.0m, 5.0m, actualDate);
            
            _salesRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Sale(1, 1, "Banana", 4.0m, 5.0m, actualDate, 20.0m));

            var result = _salesDomainService.GetSaleById(1);

            Check.That(result.ProductId).IsEqualTo(expectedSaleMessage.ProductId);
            Check.That(result.ProductName).IsEqualTo(expectedSaleMessage.ProductName);
            Check.That(result.Quantity).IsEqualTo(expectedSaleMessage.Quantity);
            Check.That(result.Price).IsEqualTo(expectedSaleMessage.Price);
            Check.That(result.Date).IsEqualTo(expectedSaleMessage.Date);
        }

        [Fact]
        public void TestGetSaleById_SaleDoesNotExist_ReturnsNull()
        {
            _salesRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((Sale)null);

            var result = _salesDomainService.GetSaleById(1);

            Check.That(result).IsEqualTo(null);
        }

        [Fact]
        public void TestSaveAsync_AddAsyncIsCalled()
        {
            _salesRepository
                .Setup(x => x.AddAsync(It.IsAny<Sale>()));

            var result = _salesDomainService.SaveAsync(new SaleMessage(2, "Apple", 6.0m, 7.0m, actualDate));

            _salesRepository.Verify(x => x.AddAsync(It.IsAny<Sale>()), Times.Once);
        }

        [Fact]
        public void TestGenerateSalesReport_ReturnsExpectedSalesReport()
        {
            var sales = new List<Sale> 
            {
                new (1, 1, "Banana", 4.0m, 5.0m, actualDate, 20.0m),
                new (2, 1, "Banana", 5.0m, 5.0m, actualDate, 25.0m),
                new (3, 2, "Apple", 6.0m, 7.0m, actualDate, 42.0m),
                new (4, 2, "Apple", 3.0m, 2.0m, actualDate.Date.AddDays(-1), 6.0m)
            };
            _salesRepository
                .Setup(x => x.GetByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult<IEnumerable<Sale>>(sales));
            var expectedResult = new List<SaleMessage>
            {
                new (1, "Banana", 9.0m, 5.0m, actualDate),
                new (2, "Apple", 6.0m, 7.0m, actualDate),
                new (2, "Apple", 3.0m, 2.0m, actualDate.Date.AddDays(-1))
            };

            var result = _salesDomainService.GenerateSalesReport(actualDate, actualDate);

            _salesRepository.Verify(x => x.GetByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
            
            Check.That(result.Result.Count()).IsEqualTo(expectedResult.Count());
            Check.That(result.Result.First().ProductId).IsEqualTo(expectedResult.First().ProductId);
            Check.That(result.Result.First().Quantity).IsEqualTo(expectedResult.First().Quantity);

            Check.That(result.Result.ElementAt(1).ProductId).IsEqualTo(expectedResult.ElementAt(1).ProductId);
            Check.That(result.Result.ElementAt(1).Date).IsEqualTo(expectedResult.ElementAt(1).Date.Date);
            Check.That(result.Result.ElementAt(1).Quantity).IsEqualTo(expectedResult.ElementAt(1).Quantity);

            Check.That(result.Result.ElementAt(2).ProductId).IsEqualTo(expectedResult.ElementAt(2).ProductId);
            Check.That(result.Result.ElementAt(2).Date).IsEqualTo(expectedResult.ElementAt(2).Date.Date);
            Check.That(result.Result.ElementAt(2).Quantity).IsEqualTo(expectedResult.ElementAt(2).Quantity);
        }

        [Fact]
        public void TestGenerateShoppingList_ReturnsExpectedGenerateShoppingList()
        {
            var sales = new List<Sale> 
            {
                new (1, 1, "Banana", 8.0m, 5.0m, actualDate.Date.AddDays(-4), 40.0m),
                new (2, 1, "Banana", 20.0m, 5.0m, actualDate.Date.AddDays(-3), 125.0m),
                new (3, 2, "Apple", 6.0m, 7.0m, actualDate.Date.AddDays(-2), 42.0m),
                new (4, 2, "Apple", 8.0m, 2.0m, actualDate.Date.AddDays(-1), 16.0m)
            };
            _salesRepository
                .Setup(x => x.GetByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult<IEnumerable<Sale>>(sales));
            var expectedResult = new List<SaleMessage>
            {
                new (1, "Banana", 4.0m, 5.0m, actualDate),
                new (2, "Apple", 2.0m, 7.0m, actualDate)
            };

            var result = _salesDomainService.GenerateShoppingList(null, null);

            _salesRepository.Verify(x => x.GetByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
            
            Check.That(result.Result.Count()).IsEqualTo(expectedResult.Count());
            Check.That(result.Result.First().ProductId).IsEqualTo(expectedResult.First().ProductId);
            Check.That(result.Result.First().Quantity).IsEqualTo(expectedResult.First().Quantity);
            Check.That(result.Result.ElementAt(1).ProductId).IsEqualTo(expectedResult.ElementAt(1).ProductId);
            Check.That(result.Result.ElementAt(1).Quantity).IsEqualTo(expectedResult.ElementAt(1).Quantity);
        }

        [Fact]
        public void TestGenerateShoppingList_InitialDateIsHigherThanFinalDate_ReturnsExpectedGenerateShoppingList()
        {
            var sales = new List<Sale> 
            {
                new (3, 2, "Apple", 6.0m, 7.0m, actualDate.Date.AddDays(-2), 42.0m),
                new (4, 2, "Apple", 8.0m, 2.0m, actualDate.Date.AddDays(-1), 16.0m)
            };
            _salesRepository
                .Setup(x => x.GetByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult<IEnumerable<Sale>>(sales));
            var expectedResult = new List<SaleMessage>
            {
                new (2, "Apple", 7.0m, 7.0m, actualDate.Date.AddDays(-1))
            };

            var result = _salesDomainService.GenerateShoppingList(actualDate.Date, actualDate.Date.AddDays(-2));

            _salesRepository.Verify(x => x.GetByPeriod(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
            
            Check.That(result.Result.Count()).IsEqualTo(expectedResult.Count());
            Check.That(result.Result.First().ProductId).IsEqualTo(expectedResult.First().ProductId);
            Check.That(result.Result.First().Quantity).IsEqualTo(expectedResult.First().Quantity);
        }
    }
}