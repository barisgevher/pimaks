using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIMAKS.Services
{
    public class TedarikciService : ITedarikciService
    {
        private readonly PimaksDbContext _context;

        public TedarikciService(PimaksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TedarikciDto>> GetAllTedarikcilerAsync()
        {
            
            return await _context.Tedarikcis
                .Include(t => t.Firma)
                .Select(t => new TedarikciDto
                {
                    TedarikciId = t.TedarikciId,
                    FirmaId = t.FirmaId,
                    FirmaAdi = t.Firma.FirmaAdi 
                })
                .ToListAsync();
        }
    }
}