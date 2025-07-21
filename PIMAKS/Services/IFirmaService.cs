using PIMAKS.DTOs;

namespace PIMAKS.Services
{
    public interface IFirmaService
    {
        Task<IEnumerable<FirmaDto>> GetAllFirmalarAsync();
      
        Task<FirmaDto> CreateFirmaAsync(FirmaDto dto);
      
    }
}
