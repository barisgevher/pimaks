using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Sahis
{
    public int SahisId { get; set; }

    public int FirmaId { get; set; }

    public string SahisAdi { get; set; } = null!;

    public string? SahisTc { get; set; }

    public string SahisTelefon { get; set; } = null!;

    public string SahisMail { get; set; } = null!;

    public virtual Firma Firma { get; set; } = null!;

    public virtual ICollection<Nakliye> Nakliyes { get; set; } = new List<Nakliye>();
}
