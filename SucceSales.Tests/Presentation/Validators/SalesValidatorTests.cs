namespace SucceSales.Tests.Presentation.Controllers
{
    using NFluent;
    using SucceSales.Presentation.DTOs;
    using SucceSales.Presentation.Validators;
    using System;
    using Xunit;

    public class SalesValidatorTests
    {
        private DateTime DefaultDateTime = DateTime.Now;

        [Fact]
        public void TestValidateSale_PriceIsZeroOrSmaller_ReturnsPriceErrorMessage()
        {
            var saleDTO = CreateSalesDTO(DefaultDateTime, Price: 0m);
            var result = SalesValidator.ValidateSale(saleDTO);

            Check.That(result).IsInstanceOf<string>();
            Check.That(result).IsEqualTo($"Sale price for {saleDTO.ProductName} can not be 0 or smaller.");
        }

        private SalesDTO CreateSalesDTO(DateTime Date, int ProductId = 1, string ProductName = "Test Product", decimal Quantity = 1m, decimal Price = 1m, decimal TotalImport = 1m)
        {
            return new SalesDTO(ProductId, ProductName, Quantity, Price, Date, TotalImport);
        }
    }
}