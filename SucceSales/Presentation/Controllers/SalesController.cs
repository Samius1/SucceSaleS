namespace SucceSales.Presentation.Controllers
{
    using SucceSales.Storage;
    using SucceSales.Storage.Entities;
    using DTOs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using Validators;
    
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly SucceSalesContext _context;

        public SalesController(ILogger<SalesController> logger, SucceSalesContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sales>> GetSale(int id)
        {
            var actualSale = await _context.Sales.FindAsync(id);

            if (actualSale == null)
            {
                return NotFound();
            }

            return actualSale;
        }

        [HttpPost]
        public async Task<ActionResult<SalesDTO>> PostSale(SalesDTO sale)
        {
            SalesValidator.ValidateSale(sale);
            
            var mappedSale = new Sales(0, sale.ProductId, sale.ProductName, sale.Quantity, sale.Price, sale.Date, sale.TotalImport);

            _context.Sales.Add(mappedSale);
            
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(PostSale), new { id = mappedSale.Id }, sale);
        }
    }
}