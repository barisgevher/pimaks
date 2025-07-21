using PIMAKS.DTOs;

namespace PIMAKS.Services
{
    public interface INakliyeService
    {

        Task<IEnumerable<NakliyeDto>> GetAllAsync();
        Task<NakliyeDto> CreateAsync(NakliyeDto dto);
    }
}
