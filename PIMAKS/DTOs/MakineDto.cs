using PIMAKS.Models;

namespace PIMAKS.DTOs
{
    public class MakineDto
    {
        public int MakineId { get; set; }

        public int TedarikciId { get; set; }

        public int TipId { get; set; }

        public int YakitId { get; set; }

        public int MarkaId { get; set; }

        public string MakineKodu { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string SeriNo { get; set; } = null!;

        public int ImalatYili { get; set; }

        public short KaldirmaKapasitesiKg { get; set; }

        public byte CalismaYuksekligi { get; set; }

        public bool KiradaMi { get; set; }

        public int BirimFiyat { get; set; }

        public string? CalismaYuzdesi { get; set; }

        public string? MarkaAdi { get; set; }

        public string? TipAdi { get; set; }

        public string? YakitAdi {  get; set; }

      
    }
}
