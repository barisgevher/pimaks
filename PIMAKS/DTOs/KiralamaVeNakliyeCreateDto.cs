namespace PIMAKS.DTOs
{
    public class KiralamaVeNakliyeCreateDto
    {
        public int MakineId { get; set; }
        public int FirmaId { get; set; } // Kiralayan firma
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string CalismaAdresi { get; set; }

        public int NakliyeFirmasiId { get; set; } // Nakliyeyi yapan firma
        public int NakliyeciSahisId { get; set; } // Nakliyeyi yapan şoför
        public decimal NakliyeUcreti { get; set; } // int yerine decimal daha doğru
    }
}
