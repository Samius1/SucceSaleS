namespace SucceSales.Presentation.Validators
{
    using DTOs;

    public static class SalesValidator
    {
        public static string ValidateSale(SalesDTO sale)
        {
            var errors = string.Empty;

            if (sale.Price <= 0)
            {
                errors = $"Sale price for {sale.ProductName} can not be 0 or smaller.";
            }

            return errors;
        }
    }
}