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

        public async Task<SaleMessage> GetSaleByIdAsync(int Id)
        {
            var sale = await _salesRepository.GetByIdAsync(Id);

            return new SaleMessage(sale.ProductId, sale.ProductName, sale.Quantity, sale.Price, sale.Date);
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
                            .GroupBy(x => new { x.Date, x.ProductId })
                            .Select(x => new SaleMessage(x.))
                            .ToList())
            {
                //saleMessages.Add(new SaleMessage(sale.productId, sale.ProductName, sale.Quantity, sale. Price, sale.Date));
                saleMessages.Add(sale);
            }

            return saleMessages;
        }
    }
}
