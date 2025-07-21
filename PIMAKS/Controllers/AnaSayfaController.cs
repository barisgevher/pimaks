using Microsoft.AspNetCore.Mvc;
using PIMAKS.DTOs;
using PIMAKS.Services;

namespace PIMAKS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnaSayfaController : ControllerBase
    {
        private readonly IAnaSayfaService _anaSayfaService;

        public AnaSayfaController(IAnaSayfaService anaSayfaService)
        {
            _anaSayfaService = anaSayfaService;
        }

       [HttpGet]
       public async Task<ActionResult<AnaSayfaDto>> Get()
        {
            var result = await _anaSayfaService.GetOzetAsync();
            return Ok(result);
        }
    }
}
