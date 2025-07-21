using PIMAKS.Models;

namespace PIMAKS.DTOs
{
    public class KiralamaDto
    {

        public int KiralamaId { get; set; }

        public int FirmaId { get; set; }

        public int MakineId { get; set; }

        public int NakliyeId { get; set; }

        public DateTime BaslangicTarihi { get; set; }

        public string CalismaAdresi { get; set; } = null!;

        public DateTime BitisTarihi { get; set; }

        public virtual Firma Firma { get; set; } = null!;

        public virtual Makine Makine { get; set; } = null!;

        public virtual Nakliye Nakliye { get; set; } = null!;

        
        public string? MakineKodu { get; set; }
        public string? FirmaAdi { get; set; }
       
    }
}
