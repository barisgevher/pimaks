using PIMAKS.DTOs;

namespace PIMAKS.Services
{
    public interface ISahisService
    {
        Task<IEnumerable<SahisDto>> GetAllSahislarAsync(string? searchTerm = null, int? firmaId = null);
      

        Task<SahisDto> CreateSahisAsync(SahisDto dto);
    }
}
