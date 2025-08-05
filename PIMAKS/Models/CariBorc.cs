using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMAKS.Models;

public partial class CariBorc
{
    public int CariBorcId { get; set; }

    public int FirmaId { get; set; }

    [Column(TypeName = "decimal(18, 2)")] // Veritabanında doğru tipi garantiler
    public decimal Tutar { get; set; }

    public virtual Firma Firma { get; set; } = null!;

    public virtual ICollection<Tahsilat> Tahsilats { get; set; } = new List<Tahsilat>();
}
