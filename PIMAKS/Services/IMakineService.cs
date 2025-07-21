using PIMAKS.DTOs;

namespace PIMAKS.Services
{
    public interface IMakineService
    {
        Task<IEnumerable<MakineDto>> GetAllAsync();
        Task<MakineDto> CreateAsync(MakineDto dto);
    }
}
