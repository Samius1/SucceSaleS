namespace SucceSales.Domain.DomainModel
{
    using Microsoft.EntityFrameworkCore;
    
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        public DbSet<Sales> AllSales { get; set; }
    }
}