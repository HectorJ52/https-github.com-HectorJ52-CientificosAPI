using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CientificosAPI.Models;

public partial class CientificosContext : DbContext
{
    public CientificosContext()
    {
    }

    public CientificosContext(DbContextOptions<CientificosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cientifico> Cientificos { get; set; }

    public virtual DbSet<Descubrimiento> Descubrimientos { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Investigacione> Investigaciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=CientificosDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cientifico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cientifi__3214EC079B72D985");

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Nacionalidad).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasMany(d => d.Especialidads).WithMany(p => p.Cientificos)
                .UsingEntity<Dictionary<string, object>>(
                    "CientificoEspecialidad",
                    r => r.HasOne<Especialidade>().WithMany()
                        .HasForeignKey("EspecialidadId")
                        .HasConstraintName("FK__Cientific__Espec__4F7CD00D"),
                    l => l.HasOne<Cientifico>().WithMany()
                        .HasForeignKey("CientificoId")
                        .HasConstraintName("FK__Cientific__Cient__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("CientificoId", "EspecialidadId").HasName("PK__Cientifi__AA8141A07F10F1D4");
                        j.ToTable("CientificoEspecialidad");
                    });
        });

        modelBuilder.Entity<Descubrimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Descubri__3214EC07032F09EC");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Lugar).HasMaxLength(100);
            entity.Property(e => e.Titulo).HasMaxLength(150);

            entity.HasOne(d => d.Cientifico).WithMany(p => p.Descubrimientos)
                .HasForeignKey(d => d.CientificoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Descubrim__Cient__52593CB8");
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Especial__3214EC07F710F881");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Investigacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Investig__3214EC07A8AC58D8");

            entity.Property(e => e.AreaEstudio).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.Titulo).HasMaxLength(150);

            entity.HasOne(d => d.Cientifico).WithMany(p => p.Investigaciones)
                .HasForeignKey(d => d.CientificoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Investiga__Cient__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
