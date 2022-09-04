namespace SucceSales.Tests.Storage.Repositories
{
    using NFluent;
    using Microsoft.EntityFrameworkCore;
    using SucceSales.Storage;
    using SucceSales.Storage.Entities;
    using SucceSales.Storage.Repositories;
    using System;
    using System.Linq;
    using Xunit;

    public class SalesRepositoryTests
    {
        private readonly SalesRepository _salesRepository;
        private static readonly DateTime DefaultDate = DateTime.Now;
        private readonly Sale _firstSale = new Sale(0, 1, "Banana", 3m, 5m, DefaultDate, 15m);

        public SalesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SucceSalesContext>()
                .UseInMemoryDatabase("AllSales").Options;

            var salesContext = new SucceSalesContext(options);
            salesContext.Sales.Add(_firstSale);
            salesContext.SaveChanges();

            _salesRepository = new SalesRepository(salesContext);
        }

        [Fact]
        public void TestGetById_SaleExists_ReturnSale()
        {
            var result = _salesRepository.GetById(1);

            Check.That(result).IsNotEqualTo(null);
            Check.That(result.Id).IsEqualTo(1);
            Check.That(result.ProductId).IsEqualTo(_firstSale.ProductId);
            Check.That(result.ProductName).IsEqualTo(_firstSale.ProductName);
            Check.That(result.Quantity).IsEqualTo(_firstSale.Quantity);
            Check.That(result.Price).IsEqualTo(_firstSale.Price);
            Check.That(result.Date).IsEqualTo(_firstSale.Date);
            Check.That(result.TotalImport).IsEqualTo(_firstSale.TotalImport);
        }

        [Fact]
        public void TestAddAsync_SaleExists_ReturnSale()
        {
            var result = _salesRepository.AddAsync(_firstSale);

            var insertedSale = _salesRepository.GetById(2);
            Check.That(insertedSale).IsNotEqualTo(null);
            Check.That(insertedSale.Id).IsEqualTo(2);
            Check.That(insertedSale.ProductId).IsEqualTo(_firstSale.ProductId);
            Check.That(insertedSale.ProductName).IsEqualTo(_firstSale.ProductName);
            Check.That(insertedSale.Quantity).IsEqualTo(_firstSale.Quantity);
            Check.That(insertedSale.Price).IsEqualTo(_firstSale.Price);
            Check.That(insertedSale.Date).IsEqualTo(_firstSale.Date);
            Check.That(insertedSale.TotalImport).IsEqualTo(_firstSale.TotalImport);
        }

        [Fact]
        public void TestGetByPeriod_ReturnSale()
        {
            var result = _salesRepository.GetByPeriod(DateTime.Now.Date.AddDays(-2), DateTime.Now.Date.AddDays(1));

            Check.That(result.Result).IsNotEqualTo(null);
            Check.That(result.Result.Count()).IsEqualTo(1);
            Check.That(result.Result.First().Id).IsEqualTo(1);
            Check.That(result.Result.First().ProductId).IsEqualTo(_firstSale.ProductId);
            Check.That(result.Result.First().ProductName).IsEqualTo(_firstSale.ProductName);
            Check.That(result.Result.First().Quantity).IsEqualTo(_firstSale.Quantity);
            Check.That(result.Result.First().Price).IsEqualTo(_firstSale.Price);
            Check.That(result.Result.First().Date).IsEqualTo(_firstSale.Date);
            Check.That(result.Result.First().TotalImport).IsEqualTo(_firstSale.TotalImport);
        }
    }
}