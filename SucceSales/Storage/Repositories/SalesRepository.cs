namespace SucceSales.Storage.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class SalesRepository : ISalesRepository
    {
        protected DbContext _dbContext;
        
        public SalesRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Sale> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Sale>().AsNoTracking().FindAsync(id);
        }
        
        public Task AddAsync(Sale sale)
        {
            _dbContext.Entry(sale).State = EntityState.Detached;
            await _dbContext.Set<Sale>().AddAsync(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetByPeriod(DateTime start, DateTime end)
        {
            return await dbContext.Set<Sale>().AsNoTracking().Where(x => x.Date >= start && x.Date <= end).ToListAsync();    
        }
    }
}
