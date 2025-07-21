using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Yakit
{
    public int YakitId { get; set; }

    public string Yakit1 { get; set; } = null!;

    public virtual ICollection<Makine> Makines { get; set; } = new List<Makine>();
}
