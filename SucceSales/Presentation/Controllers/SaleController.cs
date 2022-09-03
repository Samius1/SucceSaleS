namespace SucceSales.Presentation.Controllers
{
    using SucceSales.Domain.Entities;
    using SucceSales.Domain.Services;
    using DTOs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Validators;
    
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly ISaleDomainService _salesDomainService;

        public SalesController(ILogger<SalesController> logger, ISaleDomainService salesDomainService)
        {
            _logger = logger;
            _salesDomainService = salesDomainService;
        }

        [HttpGet("{id}")]
        public ActionResult<SaleDTO> GetSale(int id)
        {
            var actualSale = _salesDomainService.GetSaleById(id);

            if (actualSale == null)
            {
                return NotFound();
            }

            return Ok(new SaleDTO(actualSale.ProductId, actualSale.ProductName, actualSale.Quantity, actualSale.Price, actualSale.Date));
        }

        [HttpPost]
        public async Task<ActionResult> PostSale(SaleDTO sale)
        {
            SalesSyntacticValidator.ValidateSale(sale);

            await _salesDomainService.SaveAsync(new SaleMessage(sale.ProductId, sale.ProductName, sale.Quantity, sale.Price, sale.Date));
            
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<ReportDTO>> GetShoppingList(DateTime? initialDate, DateTime? finalDate)
        {   
            var sales = await _salesDomainService.GenerateShoppingList(initialDate, finalDate);
            return ConvertSaleMessagesToReportDTOList(sales);
        }

        [HttpGet]
        public async Task<IEnumerable<ReportDTO>> GetSalesReport(DateTime initialDate, DateTime finalDate)
        {   
            var sales = await _salesDomainService.GenerateSalesReport(initialDate, finalDate);
            return ConvertSaleMessagesToReportDTOList(sales);
        }

        private List<ReportDTO> ConvertSaleMessagesToReportDTOList(IEnumerable<SaleMessage> saleMessages)
        {
            var completeReport = new List<ReportDTO>();
            foreach (var saleMessage in saleMessages)
            {
                completeReport.Add(new ReportDTO(saleMessage.ProductId, saleMessage.ProductName, saleMessage.Quantity, saleMessage.Date));
            }
            return completeReport;
        }
    }
}