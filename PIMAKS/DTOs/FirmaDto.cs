using PIMAKS.Models;

namespace PIMAKS.DTOs
{
    public class FirmaDto
    {
        public int FirmaId { get; set; }

        public int SahisId { get; set; }

        public string FirmaAdi { get; set; } = null!;

        public string VergiDairesi { get; set; } = null!;

        public string VergiNumarasi { get; set; } = null!;

        public string AdresIl { get; set; } = null!;

        public string AdresIlce { get; set; } = null!;

        public string Adres { get; set; } = null!;

        public string MailAdresi { get; set; } = null!;

        public string TelefonNo { get; set; } = null!;       

        public bool TedarikciMi { get; set; }

        public decimal ToplamCariBorc { get; set; } = 0;
  

    }
}
