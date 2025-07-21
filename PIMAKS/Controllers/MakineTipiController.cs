using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIMAKS.Models;

namespace PIMAKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakineTipiController : ControllerBase
    {
        private readonly PimaksDbContext _context;

        public MakineTipiController(PimaksDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMarkalar()
        {

            var makineTipleri = await _context.MakineTipis.Select(m => new
            {
                m.TipId,
                m.MakineTipi1
            }).ToListAsync();

            return Ok(makineTipleri);
        }
    }
}
