using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PIMAKS.DTOs;
using PIMAKS.Services;

namespace PIMAKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IstatistikController : ControllerBase
    {
        private readonly IIstatistikService _istatistikService;

        public IstatistikController(IIstatistikService istatistikService)
        {
            _istatistikService = istatistikService;
        }

        [HttpPost("makine")] // POST /api/istatistik/makine
        public async Task<IActionResult> GetMakineIstatistik([FromBody] MakineIstatistikRequestDto request)
        {
            var result = await _istatistikService.GetMakineIstatistikAsync(request);
            return Ok(result);
        }

        [HttpGet("makine-genel")]
        public async Task<IActionResult> GetMakineGenelIstatistik()
        {
            var result = await _istatistikService.GetAllMakineIstatistikAsync();
            return Ok(result);
        }
    }
}
