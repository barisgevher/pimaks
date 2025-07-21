using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Kiralama
{
    public int KiralamaId { get; set; }

    public int FirmaId { get; set; }

    public int MakineId { get; set; }

    public int NakliyeId { get; set; }

    public DateTime BaslangicTarihi { get; set; }

    public string CalismaAdresi { get; set; } = null!;

    public DateTime BitisTarihi { get; set; }

    public virtual Firma Firma { get; set; } = null!;

    public virtual Makine Makine { get; set; } = null!;

    public virtual Nakliye Nakliye { get; set; } = null!;
}
