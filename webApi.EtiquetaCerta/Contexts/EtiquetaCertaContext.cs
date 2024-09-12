using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Contexts;

public partial class EtiquetaCertaContext : DbContext
{
    public EtiquetaCertaContext()
    {
    }

    public EtiquetaCertaContext(DbContextOptions<EtiquetaCertaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConservationProcess> ConservationProcesses { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<LabelSymbology> LabelSymbologies { get; set; }

    public virtual DbSet<Legislation> Legislations { get; set; }

    public virtual DbSet<ProcessInLegislation> ProcessInLegislations { get; set; }

    public virtual DbSet<Symbology> Symbologies { get; set; }

    public virtual DbSet<SymbologyTranslate> SymbologyTranslates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-O595EET;database=EtiquetaCerta;user id=sa; Pwd=Senai@134;Trustservercertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConservationProcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conserva__3213E83FB338C522");

            entity.ToTable("ConservationProcess");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Label__3213E83FE1C83BBD");

            entity.ToTable("Label");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IdLegislation).HasColumnName("id_legislation");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdLegislationNavigation).WithMany(p => p.Labels)
                .HasForeignKey(d => d.IdLegislation)
                .HasConstraintName("FK__Label__id_legisl__5BE2A6F2");
        });

        modelBuilder.Entity<LabelSymbology>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LabelSym__3213E83F713A94B9");

            entity.ToTable("LabelSymbology");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdLabel).HasColumnName("id_label");
            entity.Property(e => e.IdSymbology).HasColumnName("id_symbology");

            entity.HasOne(d => d.IdLabelNavigation).WithMany(p => p.LabelSymbologies)
                .HasForeignKey(d => d.IdLabel)
                .HasConstraintName("FK__LabelSymb__id_la__60A75C0F");

            entity.HasOne(d => d.IdSymbologyNavigation).WithMany(p => p.LabelSymbologies)
                .HasForeignKey(d => d.IdSymbology)
                .HasConstraintName("FK__LabelSymb__id_sy__5FB337D6");
        });

        modelBuilder.Entity<Legislation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Legislat__3213E83FF469F4A9");

            entity.ToTable("Legislation");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OfficialLanguage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("official_language");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ProcessInLegislation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProcessI__3213E83F0E485BAA");

            entity.ToTable("ProcessInLegislation");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdLegislation).HasColumnName("id_legislation");
            entity.Property(e => e.IdProcess).HasColumnName("id_process");

            entity.HasOne(d => d.IdLegislationNavigation).WithMany(p => p.ProcessInLegislations)
                .HasForeignKey(d => d.IdLegislation)
                .HasConstraintName("FK__ProcessIn__id_le__6A30C649");

            entity.HasOne(d => d.IdProcessNavigation).WithMany(p => p.ProcessInLegislations)
                .HasForeignKey(d => d.IdProcess)
                .HasConstraintName("FK__ProcessIn__id_pr__693CA210");
        });

        modelBuilder.Entity<Symbology>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Symbolog__3213E83FAB523C4C");

            entity.ToTable("Symbology");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdProcess).HasColumnName("id_process");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("url");

            entity.HasOne(d => d.IdProcessNavigation).WithMany(p => p.Symbologies)
                .HasForeignKey(d => d.IdProcess)
                .HasConstraintName("FK__Symbology__id_pr__5165187F");
        });

        modelBuilder.Entity<SymbologyTranslate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Symbolog__3213E83FD28FBC00");

            entity.ToTable("SymbologyTranslate");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.IdLegislation).HasColumnName("id_legislation");
            entity.Property(e => e.IdSymbology).HasColumnName("id_symbology");
            entity.Property(e => e.SymbologyTranslate1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("symbology_translate");

            entity.HasOne(d => d.IdLegislationNavigation).WithMany(p => p.SymbologyTranslates)
                .HasForeignKey(d => d.IdLegislation)
                .HasConstraintName("FK__Symbology__id_le__656C112C");

            entity.HasOne(d => d.IdSymbologyNavigation).WithMany(p => p.SymbologyTranslates)
                .HasForeignKey(d => d.IdSymbology)
                .HasConstraintName("FK__Symbology__id_sy__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
