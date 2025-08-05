namespace PIMAKS.DTOs
{
    public class MakineIstatistikRequestDto
    {
        public int MakineId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
    }
}
