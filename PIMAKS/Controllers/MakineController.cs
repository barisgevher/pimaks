using Microsoft.AspNetCore.Mvc;
using PIMAKS.DTOs;
using PIMAKS.Services;

[ApiController]
[Route("api/[controller]")]
public class MakineController : ControllerBase
{
    private readonly IMakineService _service;

    public MakineController(IMakineService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MakineDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }
}
