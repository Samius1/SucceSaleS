namespace SucceSales.Domain.Services
{
    using SucceSales.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public interface ISaleDomainService
    {
        Task<SaleMessage> GetSaleByIdAsync(int id);

        Task SaveAsync(SaleMessage saleMessage);

        Task<IEnumerable<SaleMessage>> GenerateSalesReport(DateTime initialDate, DateTime finalDate);
    }
}
