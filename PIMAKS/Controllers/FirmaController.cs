using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIMAKS.DTOs;
using PIMAKS.Services;

namespace PIMAKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirmaController : ControllerBase
    {

        private readonly IFirmaService _service;

        public FirmaController(IFirmaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllFirmalarAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FirmaDto dto)
        {
            var result = await _service.CreateFirmaAsync(dto);
            return Ok(result);
        }
    }
}
