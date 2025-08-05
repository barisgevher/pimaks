using PIMAKS.DTOs;

namespace PIMAKS.Services
{
    public interface IIstatistikService
    {
        Task<MakineIstatistikResponseDto> GetMakineIstatistikAsync(MakineIstatistikRequestDto request);
        Task<IEnumerable<MakineGenelIstatistikDto>> GetAllMakineIstatistikAsync();
    }


}
