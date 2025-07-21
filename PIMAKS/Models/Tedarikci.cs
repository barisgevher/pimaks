using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Tedarikci
{
    public int TedarikciId { get; set; }

    public int FirmaId { get; set; }

    public virtual Firma Firma { get; set; } = null!;

    public virtual ICollection<Makine> Makines { get; set; } = new List<Makine>();
}
