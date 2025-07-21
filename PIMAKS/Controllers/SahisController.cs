using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;
using PIMAKS.Services;

namespace PIMAKS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SahisController : ControllerBase
{
    private readonly ISahisService _service;

    public SahisController(ISahisService service)
    {
        _service = service;
    }

    // SahisController.cs içinde
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SahisDto>>> GetAllSahislar([FromQuery] string? searchTerm, [FromQuery] int? firmaId)
    {
        var sahislar = await _service.GetAllSahislarAsync(searchTerm, firmaId);
        return Ok(sahislar);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SahisDto dto)
    {
        var result = await _service.CreateSahisAsync(dto);
        return Ok(result);
    }

}
