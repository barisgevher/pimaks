namespace PIMAKS.DTOs
{
    public class MakineGenelIstatistikDto
    {
        public int MakineId { get; set; }
        public string MakineKodu { get; set; }
        public string MarkaAdi { get; set; }
        public int ToplamKiralamaSayisi { get; set; }
        public decimal ToplamGetiri { get; set; }
    }
}