namespace SucceSales.Presentation.Validators
{
    using DTOs;

    public static class SalesSyntacticValidator
    {
        public static string ValidateSale(SalesDTO sale)
        {
            var errors = string.Empty;

            if (sale.Price <= 0)
            {
                errors = $"{nameof(sale.Price)} for {sale.ProductName} can not be 0 or smaller.\n";
            }

            if (sale.Quantity <= 0)
            {
                errors = $"{nameof(sale.Quantity)} for {sale.ProductName} can not be 0 or smaller.\n";
            }

            if (string.IsNullOrWhiteSpace(sale.ProductName))
            {
                errors = $"{nameof(sale.ProductName)} can not be empty.";
            }

            return errors;
        }
    }
}