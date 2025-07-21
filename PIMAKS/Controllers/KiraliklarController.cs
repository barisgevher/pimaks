using Microsoft.AspNetCore.Mvc;
using PIMAKS.DTOs;
using PIMAKS.Models;
using PIMAKS.Services;

namespace PIMAKS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KiraliklarController : ControllerBase
    {
        private readonly IKiralamaService _rentalService;

        public KiraliklarController(IKiralamaService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rentals = await _rentalService.GetAllKiralamalarAsync();
            return Ok(rentals);
        }

  

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KiralamaDto kiralamaDto)
        {
            var created = await _rentalService.CreateKiralamaAsync(kiralamaDto);
            return Ok(created);
        }

        [HttpPost("with-nakliye")]
        public async Task<IActionResult> CreateWithNakliye([FromBody] KiralamaVeNakliyeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _rentalService.CreateKiralamaAndNakliyeAsync(dto);
            return Ok(result);
        }

    
    }
}
