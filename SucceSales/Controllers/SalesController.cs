namespace SucceSales.Controllers
{
    using Domain.DomainModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly SalesContext _context;

        public SalesController(ILogger<SalesController> logger, SalesContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sales>> GetSale(int id)
        {
            var actualSale = await _context.AllSales.FindAsync(id);

            if (actualSale == null)
            {
                return NotFound();
            }

            return actualSale;
        }

        [HttpPost]
        public async Task<ActionResult<Sales>> PostSale(Sales sale)
        {
            _context.AllSales.Add(sale);
            
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
        }
    }
}