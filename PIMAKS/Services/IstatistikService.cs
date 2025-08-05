using Microsoft.EntityFrameworkCore;
using PIMAKS.DTOs;
using PIMAKS.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PIMAKS.Services
{

    public class IstatistikService : IIstatistikService
    {
        private readonly PimaksDbContext _context;

        public IstatistikService(PimaksDbContext context)
        {
            _context = context;
        }

        public async Task<MakineIstatistikResponseDto> GetMakineIstatistikAsync(MakineIstatistikRequestDto request)
        {
            // Belirtilen makinenin, verilen tarih aralığıyla çakışan tüm kiralamalarını bul.
            var ilgiliKiralamalar = await _context.Kiralamas
                .Where(k => k.MakineId == request.MakineId &&
                            k.BaslangicTarihi <= request.BitisTarihi &&
                            k.BitisTarihi >= request.BaslangicTarihi)
                .Include(k => k.Makine) // Makinenin birim fiyatını almak için
                .Include(k => k.Nakliye) // Nakliye ücretini almak için
                .ToListAsync();

            int toplamGun = 0;
            decimal toplamGetiri = 0;

            foreach (var kiralama in ilgiliKiralamalar)
            {
                // Her kiralama için, istenen tarih aralığı içindeki günleri hesapla
                var baslangic = kiralama.BaslangicTarihi > request.BaslangicTarihi ? kiralama.BaslangicTarihi : request.BaslangicTarihi;
                var bitis = kiralama.BitisTarihi < request.BitisTarihi ? kiralama.BitisTarihi : request.BitisTarihi;

                var gunSayisi = (bitis - baslangic).Days + 1;

                toplamGun += gunSayisi;

                // Getiriyi hesapla: (gün * birim fiyat) + nakliye ücreti
                decimal kiralamaGetirisi = (decimal)gunSayisi * (decimal)kiralama.Makine.BirimFiyat;
                decimal nakliyeUcreti = kiralama.Nakliye != null ? kiralama.Nakliye.NakliyeUcreti : 0;

                toplamGetiri += kiralamaGetirisi + nakliyeUcreti;
            }

            return new MakineIstatistikResponseDto
            {
                ToplamCalisilanGun = toplamGun,
                ToplamGetiri = toplamGetiri,
                KiralamaSayisi = ilgiliKiralamalar.Count
            };
        }
        public async Task<IEnumerable<MakineGenelIstatistikDto>> GetAllMakineIstatistikAsync()
        {
            // Adım 1: Gerekli tüm veriyi, karmaşık hesaplama yapmadan, veritabanından çek.
            // Bu sorgu, EF'in kolayca SQL'e çevirebileceği basit bir sorgudur.
            var makinelerVeKiralamalari = await _context.Makines
                .Include(m => m.Marka)
                .Include(m => m.Kiralamas)
                    .ThenInclude(k => k.Nakliye)
                .Select(m => new
                {
                    // DTO için ihtiyacımız olan alanları seçiyoruz
                    m.MakineId,
                    m.MakineKodu,
                    MarkaAdi = m.Marka != null ? m.Marka.Marka1 : "Belirtilmemiş", // Null kontrolü
                    m.BirimFiyat,
                    Kiralamalari = m.Kiralamas.Select(k => new
                    {
                        k.BaslangicTarihi,
                        k.BitisTarihi,
                        NakliyeUcreti = k.Nakliye != null ? k.Nakliye.NakliyeUcreti : 0
                    })
                })
                .ToListAsync();

            // Adım 2: Çektiğimiz bu veri üzerinde, C# içinde (bellekte) hesaplamaları yap.
            var sonuclar = makinelerVeKiralamalari.Select(m => new MakineGenelIstatistikDto
            {
                MakineId = m.MakineId,
                MakineKodu = m.MakineKodu,
                MarkaAdi = m.MarkaAdi,
                ToplamKiralamaSayisi = m.Kiralamalari.Count(),
                ToplamGetiri = m.Kiralamalari.Sum(k =>
                {
                    // Tarih farkını burada, C# içinde güvenle hesaplayabiliriz.
                    var gunSayisi = (k.BitisTarihi - k.BaslangicTarihi).TotalDays + 1;
                    if (gunSayisi < 1) gunSayisi = 1;

                    return ((decimal)m.BirimFiyat * (decimal)gunSayisi) + (decimal)k.NakliyeUcreti;
                })
            })
            .OrderByDescending(s => s.ToplamGetiri)
            .ToList();

            return sonuclar;
        }
    }
}