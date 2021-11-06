namespace SucceSales.Domain.DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Sales
    {       
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}