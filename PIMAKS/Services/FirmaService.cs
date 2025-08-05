using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;

namespace PIMAKS.Services
{
    public class FirmaService : IFirmaService
    {
        private readonly PimaksDbContext _context;

        public FirmaService(PimaksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FirmaDto>> GetAllFirmalarAsync()
        {
            return await _context.Firmas
                .Select(f => new FirmaDto
                {
                    FirmaId = f.FirmaId,
                    FirmaAdi = f.FirmaAdi,
                    MailAdresi = f.MailAdresi,
                    TelefonNo = f.TelefonNo,
                    AdresIl = f.AdresIl,
                    AdresIlce = f.AdresIlce,
                    ToplamCariBorc = f.CariBorcs.Sum(b => (decimal?)b.Tutar) ?? 0

                })
                .ToListAsync();

        }

        public async Task<FirmaDto> CreateFirmaAsync(FirmaDto dto)
        {
            var entity = new Firma
            {
                              
                FirmaAdi = dto.FirmaAdi,
                VergiDairesi = dto.VergiDairesi,
                VergiNumarasi = dto.VergiNumarasi,
                AdresIl = dto.AdresIl,
                AdresIlce = dto.AdresIlce,
                Adres = dto.Adres,
                MailAdresi = dto.MailAdresi,
                TelefonNo = dto.TelefonNo,    
                
            };

            _context.Firmas.Add(entity);
            if ((bool)dto.TedarikciMi)
            {
                var yeniTadarikci = new Tedarikci
                {
                    Firma = entity
                };
                _context.Add(yeniTadarikci);
            }
            await _context.SaveChangesAsync();

            dto.FirmaId = entity.FirmaId;
            return dto;
        }

       
    }
}
