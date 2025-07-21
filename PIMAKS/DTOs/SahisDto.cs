using PIMAKS.Models;

namespace PIMAKS.DTOs
{
    public class SahisDto
        
    {

        public int SahisId { get; set; }

        public string SahisAdi { get; set; } = null!;

        public string? SahisTc { get; set; }

        public string SahisTelefon { get; set; } = null!;

        public string SahisMail { get; set; } = null!;

        public virtual ICollection<Firma> Firmas { get; set; } = new List<Firma>();

        public virtual ICollection<Nakliye> Nakliyes { get; set; } = new List<Nakliye>();
    }
}
