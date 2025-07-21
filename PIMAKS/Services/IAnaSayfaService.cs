using PIMAKS.DTOs;
using System.Threading.Tasks;
namespace PIMAKS.Services
{
    public interface IAnaSayfaService
    {
        Task<int> GetFirmaSayisiAsync();
        Task<int> GetMakineSayisiAsync();
        Task<int> GetAktifKiralamaSayisiAsync();
        Task<decimal> GetToplamCariBorcAsync();

        Task<AnaSayfaDto> GetOzetAsync();
    }
}
