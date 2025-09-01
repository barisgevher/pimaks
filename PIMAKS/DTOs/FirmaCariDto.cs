namespace PIMAKS.DTOs
{
    public class FirmaCariDto
    {
        public int FirmaId { get; set; }
        public string FirmaAdi { get; set; }
        public decimal ToplamBorc { get; set; }
        public decimal ToplamTahsilat { get; set; }
        public decimal KalanBakiye => ToplamBorc - ToplamTahsilat;
    }
}
