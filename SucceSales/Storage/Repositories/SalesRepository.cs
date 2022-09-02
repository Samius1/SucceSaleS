namespace SucceSales.Storage.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SucceSales.Storage.Entities;

    public class SalesRepository : ISalesRepository
    {
        protected DbContext _dbContext;
        
        public SalesRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Sale GetById(int id)
        {
            return _dbContext.Set<Sale>().Find(id);
        }
        
        public async Task AddAsync(Sale sale)
        {
            _dbContext.Entry(sale).State = EntityState.Detached;
            await _dbContext.Set<Sale>().AddAsync(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sale>> GetByPeriod(DateTime start, DateTime end)
        {
            return await _dbContext.Set<Sale>().AsNoTracking().Where(x => x.Date >= start && x.Date <= end).ToListAsync();    
        }
    }
}
