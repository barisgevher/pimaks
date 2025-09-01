using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMAKS.Models;

public partial class Tahsilat
{
    public int TahsilatId { get; set; }

    public int CariBorcId { get; set; }

    public int FirmaId { get; set; }

    public DateTime TahsilatTarihi { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TahsilatMiktari { get; set; }

    public byte OdemeTipi { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal Kdvorani { get; set; }

    public virtual CariBorc CariBorc { get; set; } = null!;
}
