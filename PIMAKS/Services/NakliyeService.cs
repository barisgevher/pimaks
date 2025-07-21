using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;

namespace PIMAKS.Services
{
    public class NakliyeService : INakliyeService
    {

        private readonly PimaksDbContext _context;

        public NakliyeService(PimaksDbContext context) => _context = context;


        public async Task<IEnumerable<NakliyeDto>> GetAllAsync()
        {
            return await _context.Nakliyes
                 .Include(n => n.Firma)
                 .Include(n => n.Sahis)
                 .Select(n => new NakliyeDto
                 {
                     NakliyeId = n.NakliyeciId,
                     FirmaId = n.FirmaId,
                     FirmaAdi = n.Firma.FirmaAdi,
                     SahisId = n.SahisId,
                     SahisAdi = n.Sahis.SahisAdi,
                     NakliyeUcreti = n.NakliyeUcreti
                 }).ToListAsync();
        }

        public async Task<NakliyeDto> CreateAsync(NakliyeDto dto)
        {
            var entity = new Nakliye
            {
                FirmaId = dto.FirmaId,
                SahisId = dto.SahisId,
                NakliyeUcreti = (int)dto.NakliyeUcreti
            };

            _context.Nakliyes.Add(entity);
            await _context.SaveChangesAsync();

            dto.NakliyeId = entity.NakliyeciId;
            dto.FirmaAdi = (await _context.Firmas.FindAsync(dto.FirmaId))?.FirmaAdi ?? "";
            dto.SahisAdi = (await _context.Sahis.FindAsync(dto.SahisId))?.SahisAdi ?? "";

            return dto;
        }

    
    }
}
