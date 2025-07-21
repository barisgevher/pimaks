namespace PIMAKS.DTOs
{
    public class AnaSayfaDto
    {
        public int FirmaSayisi { get; set; }
        public int MakineSayisi { get; set; }
        public int AktifKiralama { get; set; }
        public decimal ToplamBorc { get; set; }
        public List<String> FirmaIsimleri { get; set; } = new();
    }
}
