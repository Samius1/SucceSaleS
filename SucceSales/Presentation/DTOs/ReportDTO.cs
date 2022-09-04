namespace SucceSales.Presentation.DTOs
{
    using System;
    
    public record ReportDTO(
        int ProductId, 
        string ProductName, 
        decimal Quantity,
        DateTime Date
    );
}