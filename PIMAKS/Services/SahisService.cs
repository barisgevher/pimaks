using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;

namespace PIMAKS.Services
{
    public class SahisService : ISahisService
    {
        private readonly PimaksDbContext _context;

        public SahisService(PimaksDbContext context)
        {
            _context = context;
        }


        public async Task<SahisDto> CreateSahisAsync(SahisDto dto)
        {
            var entitiy = new Sahis
            {
                FirmaId = dto.FirmaId,
                SahisAdi = dto.SahisAdi,
                Unvan = dto.Unvan,
                SahisTc = dto.SahisTc,
                SahisMail = dto.SahisMail,
                SahisTelefon = dto.SahisTelefon
            };

            _context.Sahis.Add(entitiy);
            await _context.SaveChangesAsync();
            dto.SahisId = entitiy.SahisId;

            return dto;
        }
        public async Task<IEnumerable<SahisDto>> GetAllSahislarAsync(string? searchTerm = null, int? firmaId = null)
        {
            
            var query = _context.Sahis.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                
                query = query.Where(s => s.SahisAdi.Contains(searchTerm));
            }


            if (firmaId.HasValue && firmaId.Value > 0)
            {
                query = query.Where(s => s.FirmaId == firmaId.Value);
            }

            
            return await query
                .Select(s => new SahisDto
                {
                    SahisId = s.SahisId,
                    SahisAdi = s.SahisAdi,
                    SahisMail = s.SahisMail,
                    SahisTc = s.SahisTc,
                    SahisTelefon = s.SahisTelefon
                })               
                .ToListAsync();
        }
    }
}
