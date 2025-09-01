using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIMAKS.DTOs;
using PIMAKS.Services;

namespace PIMAKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CariController : ControllerBase
    {
        private readonly ICariService _service;
        public CariController(ICariService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllFirmaCariAsync());

        [HttpPost("tahsilat")]
        public async Task<IActionResult> Tahsilat([FromBody] TahsilatCreateDto dto) =>           
            Ok(await _service.AddTahsilatAsync(dto));

    }

}
