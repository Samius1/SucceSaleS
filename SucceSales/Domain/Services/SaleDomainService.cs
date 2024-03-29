namespace SucceSales.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SucceSales.Domain.Entities;
    using SucceSales.Storage.Entities;
    using SucceSales.Storage.Repositories;

    public class SaleDomainService : ISaleDomainService
    {
        private readonly ISalesRepository _salesRepository;

        public SaleDomainService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public SaleMessage GetSaleById(int Id)
        {
            var sale = _salesRepository.GetById(Id);
            return sale == null ? null : new SaleMessage(sale.ProductId, sale.ProductName, sale.Quantity, sale.Price, sale.Date);
        }

        public async Task SaveAsync(SaleMessage saleMessage)
        {
            var sale = new Sale(0, saleMessage.ProductId, saleMessage.ProductName, saleMessage.Quantity,
                                saleMessage.Price, saleMessage.Date, saleMessage.Quantity * saleMessage.Price);

            await _salesRepository.AddAsync(sale);
        }

        public async Task<IEnumerable<SaleMessage>> GenerateSalesReport(DateTime initialDate, DateTime finalDate)
        {
            var sales = await _salesRepository.GetByPeriod(initialDate, finalDate);
            var saleMessages = new List<SaleMessage>();
            foreach(var sale in sales
                            .GroupBy(x => new { x.Date.Date, x.ProductId })
                            .Select(x => new { x.Key.ProductId, x.Key.Date, TotalSales = x.ToList()})
                            .ToList())
            {
                var defaultSale = sale.TotalSales.First();
                saleMessages.Add(new SaleMessage(sale.ProductId, defaultSale.ProductName, 
                                sale.TotalSales.Sum(x => x.Quantity), defaultSale.Price, sale.Date));
            }

            return saleMessages;
        }

        public async Task<IEnumerable<SaleMessage>> GenerateShoppingList(DateTime? initialDate, DateTime? finalDate)
        {
            var start = initialDate.HasValue ? initialDate.Value : DateTime.Now.AddDays(-8);
            var end = finalDate.HasValue ? finalDate.Value : DateTime.Now.Date;

            if (start > end){
                var auxDate = start;
                start = end;
                end = auxDate;
            }

            var numberOfDays = (end - start).Days;
            var sales = await _salesRepository.GetByPeriod(start, end);
            var saleMessages = new List<SaleMessage>();
            foreach(var sale in sales
                            .GroupBy(x => new { x.ProductId })
                            .Select(x => new { x.Key.ProductId, TotalSales = x.ToList()})
                            .ToList())
            {
                var defaultSale = sale.TotalSales.First();
                saleMessages.Add(new SaleMessage(sale.ProductId, defaultSale.ProductName, 
                                sale.TotalSales.Sum(x => x.Quantity)/(numberOfDays * 1.0m), defaultSale.Price, defaultSale.Date));
            }

            return saleMessages;
        }
    }
}
