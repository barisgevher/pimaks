namespace PIMAKS.DTOs
{
    public class NakliyeDto
    {
        public int NakliyeId { get; set; } 
        public int FirmaId { get; set; }
        public string FirmaAdi { get; set; } = string.Empty;
        public int SahisId { get; set; }
        public string SahisAdi { get; set; } = string.Empty;
        public decimal NakliyeUcreti { get; set; }
    }
}
