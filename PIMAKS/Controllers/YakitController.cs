using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIMAKS.Models;

namespace PIMAKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YakitController : ControllerBase
    {
        private readonly PimaksDbContext _context;

        public YakitController(PimaksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMarkalar()
        {

            var yakitTipleri = await _context.Yakits.Select(m => new
            {
                m.YakitId,
                m.Yakit1
            }).ToListAsync();

            return Ok(yakitTipleri);
        }
    }
}
