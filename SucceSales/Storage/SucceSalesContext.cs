namespace SucceSales.Storage
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using SucceSales.Storage.Entities;

    public class SucceSalesContext : DbContext
    {
        public virtual DbSet<Sales> Sales { get;set; }

        public SucceSalesContext(DbContextOptions<SucceSalesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}