namespace SucceSales.Storage
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using SucceSales.Storage.Entities;

    public class SucceSalesContext : DbContext
    {
        public virtual DbSet<Sale> Sales { get;set; }

        public SucceSalesContext(DbContextOptions<SucceSalesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}