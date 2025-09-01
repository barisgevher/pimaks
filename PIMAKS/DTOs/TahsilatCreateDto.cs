using System.ComponentModel.DataAnnotations;

namespace PIMAKS.DTOs
{
    public class TahsilatCreateDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int FirmaId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TahsilatMiktari { get; set; }

        public DateTime TahsilatTarihi { get; set; } = DateTime.UtcNow;

        public byte OdemeTipi { get; set; }
    }
}
