using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class CariBorc
{
    public int CariBorcId { get; set; }

    public int FirmaId { get; set; }

    public int CariBorc1 { get; set; }

    public virtual Firma Firma { get; set; } = null!;

    public virtual ICollection<Tahsilat> Tahsilats { get; set; } = new List<Tahsilat>();
}
