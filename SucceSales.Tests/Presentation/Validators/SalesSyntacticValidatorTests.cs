namespace SucceSales.Tests.Presentation.Controllers
{
    using NFluent;
    using SucceSales.Presentation.DTOs;
    using SucceSales.Presentation.Validators;
    using System;
    using Xunit;

    public class SalesSyntacticValidatorTests
    {
        private DateTime DefaultDateTime = DateTime.Now;

        [Fact]
        public void TestValidateSale_PriceIsZeroOrSmaller_ReturnsPriceErrorMessage()
        {
            var saleDTO = CreateSalesDTO(DefaultDateTime, Price: 0m);
            var result = SalesSyntacticValidator.ValidateSale(saleDTO);

            Check.That(result).IsInstanceOf<string>();
            Check.That(result).IsEqualTo($"Price for {saleDTO.ProductName} can not be 0 or smaller.\n");
        }

        [Fact]
        public void TestValidateSale_QuantityIsZeroOrSmaller_ReturnsQuantityErrorMessage()
        {
            var saleDTO = CreateSalesDTO(DefaultDateTime, Quantity: 0m);
            var result = SalesSyntacticValidator.ValidateSale(saleDTO);

            Check.That(result).IsInstanceOf<string>();
            Check.That(result).IsEqualTo($"Quantity for {saleDTO.ProductName} can not be 0 or smaller.\n");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void TestValidateSale_ProductNameIsNullEmptyOrWhiteSpace_ReturnsProductNameErrorMessage(string productName)
        {
            var saleDTO = CreateSalesDTO(DefaultDateTime, ProductName: productName);
            var result = SalesSyntacticValidator.ValidateSale(saleDTO);

            Check.That(result).IsInstanceOf<string>();
            Check.That(result).IsEqualTo($"ProductName can not be empty.");
        }

        [Fact]
        public void TestValidateSale_MultipleErrors_ReturnsAllErrorMessage()
        {
            var saleDTO = CreateSalesDTO(DefaultDateTime, ProductName: "", Quantity: 0, Price: 0);
            var result = SalesSyntacticValidator.ValidateSale(saleDTO);

            Check.That(result).IsInstanceOf<string>();
            Check.That(result).IsEqualTo($"Price for {saleDTO.ProductName} can not be 0 or smaller.\nQuantity for {saleDTO.ProductName} can not be 0 or smaller.\nProductName can not be empty.");
        }

        private SalesDTO CreateSalesDTO(DateTime Date, int ProductId = 1, string ProductName = "Test Product", decimal Quantity = 1m, decimal Price = 1m)
        {
            return new SalesDTO(ProductId, ProductName, Quantity, Price, Date);
        }
    }
}