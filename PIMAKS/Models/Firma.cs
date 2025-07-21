using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Firma
{
    public int FirmaId { get; set; }

    public string FirmaAdi { get; set; } = null!;

    public string VergiDairesi { get; set; } = null!;

    public string VergiNumarasi { get; set; } = null!;

    public string AdresIl { get; set; } = null!;

    public string AdresIlce { get; set; } = null!;

    public string Adres { get; set; } = null!;

    public string MailAdresi { get; set; } = null!;

    public string TelefonNo { get; set; } = null!;

    public virtual ICollection<CariBorc> CariBorcs { get; set; } = new List<CariBorc>();

    public virtual ICollection<Kiralama> Kiralamas { get; set; } = new List<Kiralama>();

    public virtual ICollection<Nakliye> Nakliyes { get; set; } = new List<Nakliye>();

    public virtual ICollection<Sahis> Sahis { get; set; } = new List<Sahis>();

    public virtual ICollection<Tedarikci> Tedarikcis { get; set; } = new List<Tedarikci>();
}
