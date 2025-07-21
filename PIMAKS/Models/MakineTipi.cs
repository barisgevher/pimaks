using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class MakineTipi
{
    public int TipId { get; set; }

    public string MakineTipi1 { get; set; } = null!;

    public virtual ICollection<Makine> Makines { get; set; } = new List<Makine>();
}
