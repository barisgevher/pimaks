using PIMAKS.DTOs;
using PIMAKS.Models;

namespace PIMAKS.Services
{
    public interface IKiralamaService
    {
        Task<IEnumerable<KiralamaDto>> GetAllKiralamalarAsync();
       
        Task<KiralamaDto> CreateKiralamaAsync(KiralamaDto kiralama);
        Task<KiralamaDto> CreateKiralamaAndNakliyeAsync(KiralamaVeNakliyeCreateDto dto);



    }
}
