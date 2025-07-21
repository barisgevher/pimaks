namespace PIMAKS.DTOs
{
    public class TahsilatDto
    {
        public int TahsilatId { get; set; }
        public int FirmaId {  get; set; }
        public decimal TahsilatMiktari {  get; set; }
        public DateTime TahsilatTarihi {  get; set; }
        public byte OdemeTipi {  get; set; }
        public int kdvOrani {  get; set; }
    }
}
