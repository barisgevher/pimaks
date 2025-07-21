using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PIMAKS.Services
{
    public class KiralamaService : IKiralamaService
    {
        private readonly PimaksDbContext _context;

        public KiralamaService(PimaksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KiralamaDto>> GetAllKiralamalarAsync()
        {
            return await _context.Kiralamas
                 .Include(k => k.Firma)
                 .Include(k => k.Makine)
                 .Select(k => new KiralamaDto
                 {
                     KiralamaId = k.KiralamaId,
                     BaslangicTarihi = k.BaslangicTarihi,
                     BitisTarihi = k.BitisTarihi,
                     CalismaAdresi = k.CalismaAdresi,
                     MakineId = k.MakineId,
                     MakineKodu = k.Makine.MakineKodu,
                     FirmaId = k.FirmaId,
                     FirmaAdi = k.Firma.FirmaAdi
                 })
                 .ToListAsync();
        }



        public async Task<KiralamaDto>  CreateKiralamaAsync(KiralamaDto dto)
        {
            var kiralama = new Kiralama
            {
                MakineId = dto.MakineId,
                FirmaId = dto.FirmaId,
                NakliyeId = dto.NakliyeId,
                BaslangicTarihi = dto.BaslangicTarihi,
                CalismaAdresi = dto.CalismaAdresi,
                BitisTarihi = dto.BitisTarihi,
            };

            _context.Kiralamas.Add(kiralama);
            await _context.SaveChangesAsync();

            return new KiralamaDto
            {
                MakineId = dto.MakineId,
                FirmaId = dto.FirmaId,
                NakliyeId = dto.NakliyeId,
                BaslangicTarihi = dto.BaslangicTarihi,
                CalismaAdresi = dto.CalismaAdresi,
                BitisTarihi = dto.BitisTarihi,
            };
        }

        public async Task<KiralamaDto> CreateKiralamaAndNakliyeAsync(KiralamaVeNakliyeCreateDto dto)
        {

            var yeniNakliye = new Nakliye
            {
                FirmaId = dto.NakliyeFirmasiId,
                NakliyeUcreti = (int)dto.NakliyeUcreti,
                SahisId = dto.NakliyeciSahisId

            };

            _context.Nakliyes.Add(yeniNakliye);

            var yeniKiralama = new Kiralama
            {
                MakineId = dto.MakineId,
                FirmaId = dto.FirmaId,
                BaslangicTarihi = dto.BaslangicTarihi,
                BitisTarihi = dto.BitisTarihi,
                CalismaAdresi = dto.CalismaAdresi,

                Nakliye = yeniNakliye
            };

            _context.Kiralamas.Add(yeniKiralama);


            await _context.SaveChangesAsync();

            var tamKiralama = await _context.Kiralamas
        .Include(k => k.Firma)
        .Include(k => k.Makine)
        .FirstOrDefaultAsync(k => k.KiralamaId == yeniKiralama.KiralamaId);

            // Eğer bir sorun olursa ve kayıt bulunamazsa, en azından boş olmayan bir DTO döndür
            if (tamKiralama == null)
            {
                // Bu bloğa girme ihtimali çok düşüktür.
                return new KiralamaDto { KiralamaId = yeniKiralama.KiralamaId };
            }

            // 5. Bu yeni ve "zengin" veriyi DTO'ya dönüştürüp React'e gönderiyoruz.
            //    Artık 'tamKiralama.Firma' ve 'tamKiralama.Makine' null olmayacak.
            var sonucDto = new KiralamaDto
            {
                KiralamaId = tamKiralama.KiralamaId,
                FirmaId = tamKiralama.FirmaId,
                FirmaAdi = tamKiralama.Firma.FirmaAdi,
                MakineId = tamKiralama.MakineId,
                MakineKodu = tamKiralama.Makine.MakineKodu,
                BaslangicTarihi = tamKiralama.BaslangicTarihi,
                BitisTarihi = tamKiralama.BitisTarihi,
                CalismaAdresi = tamKiralama.CalismaAdresi
            };

            return sonucDto;

        }

     

    }
}
