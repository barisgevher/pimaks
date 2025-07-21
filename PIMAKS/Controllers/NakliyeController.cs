using Microsoft.AspNetCore.Mvc;
using PIMAKS.DTOs;
using PIMAKS.Services;

namespace PIMAKS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NakliyeController : ControllerBase
    {
        private readonly INakliyeService _svc;
        public NakliyeController(INakliyeService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NakliyeDto dto) =>
            Ok(await _svc.CreateAsync(dto));
    }
}
