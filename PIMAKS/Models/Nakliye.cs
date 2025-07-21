using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Nakliye
{
    public int NakliyeciId { get; set; }

    public int FirmaId { get; set; }

    public int SahisId { get; set; }

    public int NakliyeUcreti { get; set; }

    public virtual Firma Firma { get; set; } = null!;

    public virtual ICollection<Kiralama> Kiralamas { get; set; } = new List<Kiralama>();

    public virtual Sahis Sahis { get; set; } = null!;
}
