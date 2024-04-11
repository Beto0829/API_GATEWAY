using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Notas.Server.Models
{
    public class MiDbContext : DbContext
    {
        public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }

        public DbSet<Nota> Notas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Nota>()
           .HasOne(n => n.Categoria)            // Una nota tiene una categoría
           .WithMany(c => c.Notas)              // Una categoría puede tener varias notas
           .HasForeignKey(n => n.IdCategoria)   // La clave foránea en Nota apunta a IdCategoria
           .IsRequired();

            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nombre)
                .IsUnique();

            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 1, Nombre = "Sin categoria" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 2, Nombre = "Devops" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 3, Nombre = "Cloud" });
        }

    }
}