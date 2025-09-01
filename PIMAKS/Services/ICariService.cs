using PIMAKS.DTOs;
using PIMAKS.Models;

namespace PIMAKS.Services
{
    public interface ICariService
    {
        Task<IEnumerable<FirmaCariDto>> GetAllFirmaCariAsync();
        Task<Tahsilat> AddTahsilatAsync(TahsilatCreateDto dto);
    }
}
