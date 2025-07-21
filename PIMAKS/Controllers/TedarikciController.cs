using Microsoft.AspNetCore.Mvc;
using PIMAKS.Services;
using System.Threading.Tasks;

namespace PIMAKS.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/tedarikci
    public class TedarikciController : ControllerBase
    {
        private readonly ITedarikciService _tedarikciService;

        public TedarikciController(ITedarikciService tedarikciService)
        {
            _tedarikciService = tedarikciService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tedarikciler = await _tedarikciService.GetAllTedarikcilerAsync();
            return Ok(tedarikciler);
        }
    }
}