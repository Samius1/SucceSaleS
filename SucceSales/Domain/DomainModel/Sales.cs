
namespace Domain.DomainModel
{
    using System;

    public class Sales : ISales
    {        
        public IProduct Product { get; set; }
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}