using PIMAKS.DTOs;

namespace PIMAKS.Services
{
    public interface ICariService
    {
        Task<IEnumerable<CariDto>> GetAllCariAsync();
        Task<TahsilatDto> AddTahsilatAsync(TahsilatDto dto);
    }
}
