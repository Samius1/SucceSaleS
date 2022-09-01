namespace SucceSales.Presentation.DTOs
{
    using System;
    
    public record SalesDTO(
        int ProductId, 
        string ProductName, 
        decimal Quantity, 
        decimal Price, 
        DateTime Date
    );
}