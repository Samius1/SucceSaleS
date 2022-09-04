namespace SucceSales.Presentation.DTOs
{
    using System;
    
    public record SaleDTO(
        int ProductId, 
        string ProductName, 
        decimal Quantity, 
        decimal Price, 
        DateTime Date
    );
}