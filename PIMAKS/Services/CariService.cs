using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIMAKS.Services
{
  

    public class CariService : ICariService
    {
        private readonly PimaksDbContext _context;
        public CariService(PimaksDbContext context) => _context = context;

        public async Task<IEnumerable<FirmaCariDto>> GetAllFirmaCariAsync()
        {
            return await _context.Firmas
                .Select(f => new FirmaCariDto
                {
                    FirmaId = f.FirmaId,
                    FirmaAdi = f.FirmaAdi,
                    // Tüm borçların toplamını al
                    ToplamBorc = f.CariBorcs.Sum(b => b.Tutar),
                    // Tüm tahsilatların toplamını al
                    ToplamTahsilat = f.Tahsilats.Sum(t => t.TahsilatMiktari)
                })
                .ToListAsync();
        }

        public async Task<Tahsilat> AddTahsilatAsync(TahsilatCreateDto dto)
        {
            var tahsilat = new Tahsilat
            {
                FirmaId = dto.FirmaId,
                TahsilatMiktari = dto.TahsilatMiktari,
                TahsilatTarihi = dto.TahsilatTarihi,
                OdemeTipi = dto.OdemeTipi,
                // CariBorcId artık direkt bağlanmıyor, çünkü bir firma geneline ödeme yapılıyor.
                // KDV gibi alanlar bu basit DTO'da şimdilik yer almıyor.
            };

            _context.Tahsilats.Add(tahsilat);
            await _context.SaveChangesAsync();
            return tahsilat;
        }
    }
}