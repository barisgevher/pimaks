using PIMAKS.DTOs;

namespace PIMAKS.Services
{
    public interface ITedarikciService
    {
        Task<IEnumerable<TedarikciDto>> GetAllTedarikcilerAsync();
    }
}
