using System;
using System.Collections.Generic;

namespace PIMAKS.Models;

public partial class Makine
{
    public int MakineId { get; set; }

    public int TedarikciId { get; set; }

    public int TipId { get; set; }

    public int YakitId { get; set; }

    public int MarkaId { get; set; }

    public string MakineKodu { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string SeriNo { get; set; } = null!;

    public int ImalatYili { get; set; }

    public short KaldirmaKapasitesiKg { get; set; }

    public byte CalismaYuksekligi { get; set; }

    public bool KiradaMi { get; set; }

    public int BirimFiyat { get; set; }

    public string? CalismaYuzdesi { get; set; }

    public virtual ICollection<Kiralama> Kiralamas { get; set; } = new List<Kiralama>();

    public virtual Marka Marka { get; set; } = null!;

    public virtual Tedarikci Tedarikci { get; set; } = null!;

    public virtual MakineTipi Tip { get; set; } = null!;

    public virtual Yakit Yakit { get; set; } = null!;
}
