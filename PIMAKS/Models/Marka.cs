using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Marka
{
    public int MarkaId { get; set; }

    public string Marka1 { get; set; } = null!;

    public virtual ICollection<Makine> Makines { get; set; } = new List<Makine>();
}
