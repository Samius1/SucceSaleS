namespace SucceSales.Storage.Entities
{
    using System;

    public class Sales
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }

        public Sales(int id, int productId, string productName, decimal quantity, decimal price, DateTime date)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            Date = date;  
        }
    }
}