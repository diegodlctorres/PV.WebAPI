using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PV.WebAPI.Models;

public partial class DbPlatoVoladorContext : DbContext
{
    public DbPlatoVoladorContext()
    {
    }

    public DbPlatoVoladorContext(DbContextOptions<DbPlatoVoladorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<IngredientesPorReceta> IngredientesPorReceta { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IngredienteId).HasName("PK__Ingredie__CCB95EC8F27877B6");

            entity.Property(e => e.IngredienteId).HasColumnName("IngredienteID");
            entity.Property(e => e.NombreIngrediente).HasMaxLength(100);
        });

        modelBuilder.Entity<IngredientesPorReceta>(entity =>
        {
            entity.HasKey(e => e.RelacionId).HasName("PK__Ingredie__4D41F2D781B90097");

            entity.Property(e => e.RelacionId).HasColumnName("RelacionID");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IngredienteId).HasColumnName("IngredienteID");
            entity.Property(e => e.RecetaId).HasColumnName("RecetaID");
            entity.Property(e => e.UnidadMedida).HasMaxLength(50);

            entity.HasOne(d => d.Ingrediente).WithMany(p => p.IngredientesPorReceta)
                .HasForeignKey(d => d.IngredienteId)
                .HasConstraintName("FK__Ingredien__Ingre__2B3F6F97");

            entity.HasOne(d => d.Receta).WithMany(p => p.IngredientesPorReceta)
                .HasForeignKey(d => d.RecetaId)
                .HasConstraintName("FK__Ingredien__Recet__2A4B4B5E");
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.HasKey(e => e.RecetaId).HasName("PK__Recetas__03D077B8B3B62FB7");

            entity.Property(e => e.RecetaId).HasColumnName("RecetaID");
            entity.Property(e => e.NombreReceta).HasMaxLength(100);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Receta)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Recetas_Usuarios_UsuarioId");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798E242FC94");

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Apellido).HasMaxLength(50);
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
