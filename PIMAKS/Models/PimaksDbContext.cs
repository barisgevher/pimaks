using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PIMAKS.Models;

public partial class PimaksDbContext : DbContext
{
    public PimaksDbContext()
    {
    }

    public PimaksDbContext(DbContextOptions<PimaksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CariBorc> CariBorcs { get; set; }

    public virtual DbSet<Firma> Firmas { get; set; }

    public virtual DbSet<Kiralama> Kiralamas { get; set; }

    public virtual DbSet<Makine> Makines { get; set; }

    public virtual DbSet<MakineTipi> MakineTipis { get; set; }

    public virtual DbSet<Marka> Markas { get; set; }

    public virtual DbSet<Nakliye> Nakliyes { get; set; }

    public virtual DbSet<Sahis> Sahis { get; set; }

    public virtual DbSet<Tahsilat> Tahsilats { get; set; }

    public virtual DbSet<Tedarikci> Tedarikcis { get; set; }

    public virtual DbSet<Yakit> Yakits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4UTOH45\\MSSQLSERVER01;Initial Catalog=pimaksDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CariBorc>(entity =>
        {
            entity.ToTable("CariBorc");

            entity.Property(e => e.CariBorcId).HasColumnName("CariBorcID");
            entity.Property(e => e.Tutar).HasColumnName("CariBorc");
            entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

            entity.HasOne(d => d.Firma).WithMany(p => p.CariBorcs)
                .HasForeignKey(d => d.FirmaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CariBorc_Firma");
        });

        modelBuilder.Entity<Firma>(entity =>
        {
            entity.ToTable("Firma");

            entity.Property(e => e.FirmaId).HasColumnName("FirmaID");
            entity.Property(e => e.Adres).HasMaxLength(1500);
            entity.Property(e => e.AdresIl).HasMaxLength(150);
            entity.Property(e => e.AdresIlce).HasMaxLength(250);
            entity.Property(e => e.FirmaAdi).HasMaxLength(500);
            entity.Property(e => e.MailAdresi).HasMaxLength(320);
            entity.Property(e => e.TelefonNo).HasMaxLength(13);
            entity.Property(e => e.VergiDairesi).HasMaxLength(500);
            entity.Property(e => e.VergiNumarasi).HasMaxLength(50);
        });

        modelBuilder.Entity<Kiralama>(entity =>
        {
            entity.ToTable("Kiralama");

            entity.Property(e => e.KiralamaId).HasColumnName("KiralamaID");
            entity.Property(e => e.BaslangicTarihi).HasPrecision(0);
            entity.Property(e => e.BitisTarihi).HasPrecision(0);
            entity.Property(e => e.CalismaAdresi).HasMaxLength(1500);
            entity.Property(e => e.FirmaId).HasColumnName("FirmaID");
            entity.Property(e => e.MakineId).HasColumnName("MakineID");
            entity.Property(e => e.NakliyeId).HasColumnName("NakliyeID");

            entity.HasOne(d => d.Firma).WithMany(p => p.Kiralamas)
                .HasForeignKey(d => d.FirmaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kiralama_Firma");

            entity.HasOne(d => d.Makine).WithMany(p => p.Kiralamas)
                .HasForeignKey(d => d.MakineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kiralama_Makine");

            entity.HasOne(d => d.Nakliye).WithMany(p => p.Kiralamas)
                .HasForeignKey(d => d.NakliyeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kiralama_Nakliye");
        });

        modelBuilder.Entity<Makine>(entity =>
        {
            entity.ToTable("Makine");

            entity.Property(e => e.MakineId).HasColumnName("MakineID");
            entity.Property(e => e.CalismaYuzdesi).HasMaxLength(50);
            entity.Property(e => e.KaldirmaKapasitesiKg).HasColumnName("KaldirmaKapasitesiKG");
            entity.Property(e => e.KiradaMi).HasColumnName("kiradaMi");
            entity.Property(e => e.MakineKodu).HasMaxLength(150);
            entity.Property(e => e.MarkaId).HasColumnName("MarkaID");
            entity.Property(e => e.Model).HasMaxLength(500);
            entity.Property(e => e.SeriNo).HasMaxLength(50);
            entity.Property(e => e.TedarikciId).HasColumnName("TedarikciID");
            entity.Property(e => e.TipId).HasColumnName("TipID");
            entity.Property(e => e.YakitId).HasColumnName("YakitID");

            entity.HasOne(d => d.Marka).WithMany(p => p.Makines)
                .HasForeignKey(d => d.MarkaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Makine_Marka");

            entity.HasOne(d => d.Tedarikci).WithMany(p => p.Makines)
                .HasForeignKey(d => d.TedarikciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Makine_Tedarikci");

            entity.HasOne(d => d.Tip).WithMany(p => p.Makines)
                .HasForeignKey(d => d.TipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Makine_MakineTipi");

            entity.HasOne(d => d.Yakit).WithMany(p => p.Makines)
                .HasForeignKey(d => d.YakitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Makine_Yakit");
        });

        modelBuilder.Entity<MakineTipi>(entity =>
        {
            entity.HasKey(e => e.TipId);

            entity.ToTable("MakineTipi");

            entity.Property(e => e.TipId).HasColumnName("TipID");
            entity.Property(e => e.MakineTipi1)
                .HasMaxLength(50)
                .HasColumnName("MakineTipi");
        });

        modelBuilder.Entity<Marka>(entity =>
        {
            entity.ToTable("Marka");

            entity.Property(e => e.MarkaId).HasColumnName("MarkaID");
            entity.Property(e => e.Marka1)
                .HasMaxLength(500)
                .HasColumnName("Marka");
        });

        modelBuilder.Entity<Nakliye>(entity =>
        {
            entity.HasKey(e => e.NakliyeciId);

            entity.ToTable("Nakliye");

            entity.Property(e => e.NakliyeciId).HasColumnName("NakliyeciID");
            entity.Property(e => e.FirmaId).HasColumnName("FirmaID");
            entity.Property(e => e.SahisId).HasColumnName("SahisID");

            entity.HasOne(d => d.Firma).WithMany(p => p.Nakliyes)
                .HasForeignKey(d => d.FirmaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nakliye_Firma");

            entity.HasOne(d => d.Sahis).WithMany(p => p.Nakliyes)
                .HasForeignKey(d => d.SahisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Nakliye_Sahis");
        });

        modelBuilder.Entity<Sahis>(entity =>
        {
            entity.HasKey(e => e.SahisId);

            entity.Property(e => e.SahisId).HasColumnName("SahisID");
            entity.Property(e => e.FirmaId).HasColumnName("FirmaID");
            entity.Property(e => e.SahisAdi).HasMaxLength(250);
            entity.Property(e => e.SahisMail).HasMaxLength(320);
            entity.Property(e => e.SahisTc).HasMaxLength(11);
            entity.Property(e => e.SahisTelefon).HasMaxLength(20);

            entity.HasOne(d => d.Firma).WithMany(p => p.Sahis)
                .HasForeignKey(d => d.FirmaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sahis_Firma");
        });

        modelBuilder.Entity<Tahsilat>(entity =>
        {
            entity.ToTable("Tahsilat");

            entity.Property(e => e.TahsilatId).HasColumnName("TahsilatID");
            entity.Property(e => e.CariBorcId).HasColumnName("CariBorcID");
            entity.Property(e => e.FirmaId).HasColumnName("FirmaID");
            entity.Property(e => e.Kdvorani).HasColumnName("KDVOrani");

            entity.HasOne(d => d.CariBorc).WithMany(p => p.Tahsilats)
                .HasForeignKey(d => d.CariBorcId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tahsilat_CariBorc");
        });

        modelBuilder.Entity<Tedarikci>(entity =>
        {
            entity.ToTable("Tedarikci");

            entity.Property(e => e.TedarikciId).HasColumnName("TedarikciID");
            entity.Property(e => e.FirmaId).HasColumnName("FirmaID");

            entity.HasOne(d => d.Firma).WithMany(p => p.Tedarikcis)
                .HasForeignKey(d => d.FirmaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tedarikci_Firma");
        });

        modelBuilder.Entity<Yakit>(entity =>
        {
            entity.ToTable("Yakit");

            entity.Property(e => e.YakitId).HasColumnName("YakitID");
            entity.Property(e => e.Yakit1)
                .HasMaxLength(50)
                .HasColumnName("Yakit");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
