using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIMAKS.Models;

namespace PIMAKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkaController : ControllerBase
    {
        private readonly PimaksDbContext _context;

        public MarkaController(PimaksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMarkalar()
        {

            var markalar = await _context.Markas.Select(m => new
            {
                m.MarkaId,
                m.Marka1
            }).ToListAsync();

            return Ok(markalar);
        }
    }
}
