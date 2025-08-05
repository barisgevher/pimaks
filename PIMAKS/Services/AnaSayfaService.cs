
using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;

namespace PIMAKS.Services
{
    public class AnaSayfaService : IAnaSayfaService
    {

        private readonly PimaksDbContext _context;

        public AnaSayfaService(PimaksDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetAktifKiralamaSayisiAsync()
        {
           return await _context.Kiralamas.CountAsync(k => k.BitisTarihi >= DateTime.Now);
        }

        public async Task<int> GetFirmaSayisiAsync()
        {
            return await _context.Firmas.CountAsync();
        }

        public async Task<int> GetMakineSayisiAsync()
        {
           return await _context.Makines.CountAsync();
        }

        public async Task<decimal> GetToplamCariBorcAsync()
        {
            return await _context.CariBorcs.SumAsync(b => (decimal?)b.Tutar) ?? 0;
        }



        public async Task<AnaSayfaDto> GetOzetAsync()
        {
            return new AnaSayfaDto
            {
                FirmaSayisi = await GetFirmaSayisiAsync(),
                MakineSayisi = await GetMakineSayisiAsync(),
                AktifKiralama = await GetAktifKiralamaSayisiAsync(),
                ToplamBorc = await GetToplamCariBorcAsync(),
                FirmaIsimleri = await _context.Firmas.Select(f => f.FirmaAdi).Take(5).ToListAsync()
            };
        }

    
    }
}
