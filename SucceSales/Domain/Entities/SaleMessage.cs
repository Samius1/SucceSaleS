namespace SucceSales.Domain.Entities
{
    using System;
    
    public class SaleMessage 
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public SaleMessage(int productId, string productName, decimal quantity, decimal price, DateTime date)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            Date = date;
        }
    }
}