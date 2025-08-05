using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;

namespace PIMAKS.Services
{
    public class CariService : ICariService
    {

        private readonly PimaksDbContext _context;
        public CariService(PimaksDbContext context) => _context = context;

        public async Task<IEnumerable<CariDto>> GetAllCariAsync()
        {
            return await _context.Firmas
                .Select(f => new CariDto
                {
                    FirmaId = f.FirmaId,
                    FirmaAdi = f.FirmaAdi,
                    ToplamBorc = f.CariBorcs.Sum(b => (decimal?)b.Tutar)?? 0
                })
                .ToListAsync();


        }

        public async Task<TahsilatDto> AddTahsilatAsync(TahsilatDto dto)
        {
            var tahsilat = new Tahsilat
            {
                FirmaId = dto.FirmaId,
                CariBorcId = (await _context.CariBorcs.FirstOrDefaultAsync(cb => cb.FirmaId == dto.FirmaId))?.CariBorcId ?? 0,
                TahsilatMiktari = (int)dto.TahsilatMiktari,
                OdemeTipi = dto.OdemeTipi,
                Kdvorani = dto.kdvOrani,
                TahsilatTarihi = dto.TahsilatTarihi
            };

            _context.Tahsilats.Add(tahsilat);
            await _context.SaveChangesAsync();
            dto.TahsilatId = tahsilat.TahsilatId;
            return dto;
        }
    }
}
