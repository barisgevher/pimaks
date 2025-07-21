using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;

namespace PIMAKS.Services
{
    public class MakineService : IMakineService
    {

        private readonly PimaksDbContext _context;

        public MakineService(PimaksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MakineDto>> GetAllAsync()
        {
            return await _context.Makines
               .Select(m => new MakineDto
               {
                   MakineId = m.MakineId,
                   MakineKodu = m.MakineKodu,
                   Model = m.Model,
                   ImalatYili = m.ImalatYili,
                   KaldirmaKapasitesiKg = m.KaldirmaKapasitesiKg,
                   BirimFiyat = m.BirimFiyat,
                   MarkaAdi = m.Marka.Marka1,
                   TipAdi = m.Tip.MakineTipi1,
                   YakitAdi = m.Yakit.Yakit1
               }).ToListAsync();
        }

        public async Task<MakineDto> CreateAsync(MakineDto dto)
        {
            var makine = new Makine
            {
                MakineKodu = dto.MakineKodu,
               
                Model = dto.Model,
                SeriNo = dto.SeriNo,
                ImalatYili = dto.ImalatYili,
                KaldirmaKapasitesiKg = dto.KaldirmaKapasitesiKg,
                CalismaYuksekligi = dto.CalismaYuksekligi,
                KiradaMi = dto.KiradaMi,
                BirimFiyat = dto.BirimFiyat,
                CalismaYuzdesi = dto.CalismaYuzdesi,
                TedarikciId = dto.TedarikciId,
                MarkaId = dto.MarkaId,
                TipId = dto.TipId,
                YakitId = dto.YakitId


            };

            _context.Makines.Add(makine);
            await _context.SaveChangesAsync();

            var yeniEklenenMakine = await _context.Makines
                .Include(m => m.Marka)
                .Include(m => m.Tip)
                .Include(m => m.Yakit)
                .FirstOrDefaultAsync(m => m.MakineId == makine.MakineId);
            
            if(yeniEklenenMakine == null)
            {
                dto.MakineId = makine.MakineId;
                return dto;
            }

            return new MakineDto
            {
                MakineId = yeniEklenenMakine.MakineId,
                MakineKodu = yeniEklenenMakine.MakineKodu,
                Model = yeniEklenenMakine.Model,
                ImalatYili = yeniEklenenMakine.ImalatYili,
                KaldirmaKapasitesiKg = yeniEklenenMakine.KaldirmaKapasitesiKg,
                BirimFiyat = yeniEklenenMakine.BirimFiyat,
                MarkaAdi = yeniEklenenMakine.Marka.Marka1,
                TipAdi = yeniEklenenMakine.Tip.MakineTipi1,
                YakitAdi = yeniEklenenMakine.Yakit.Yakit1
            };
        }
    }
}
