using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Tahsilat
{
    public int TahsilatId { get; set; }

    public int CariBorcId { get; set; }

    public int FirmaId { get; set; }

    public DateTime TahsilatTarihi { get; set; }

    public int TahsilatMiktari { get; set; }

    public byte OdemeTipi { get; set; }

    public int Kdvorani { get; set; }

    public virtual CariBorc CariBorc { get; set; } = null!;
}
