namespace SucceSales.Storage.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SucceSales.Storage.Entities;

    public interface ISalesRepository
    {
        Sale GetById(int id);
        Task AddAsync(Sale sale);
        Task<IEnumerable<Sale>> GetByPeriod(DateTime start, DateTime end);
    }
}
