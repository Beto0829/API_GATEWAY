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

            //Insertar Categoria
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 1, Nombre = "Sin categoria", Email = "admin@gmail.com" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 2, Nombre = "Ejercicio", Email = "admin@gmail.com" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 3, Nombre = "Diario", Email = "admin@gmail.com" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 4, Nombre = "Recetas", Email = "admin@gmail.com" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 5, Nombre = "Pendientes", Email = "admin@gmail.com" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 6, Nombre = "Compras pendientes", Email = "admin@gmail.com" });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { Id = 7, Nombre = "Comandos .Net", Email = "admin@gmail.com" });

            //Insertar Notas
            modelBuilder.Entity<Nota>().HasData(new Nota { Id = 1, Titulo = "Dia 16", Descripcion = "Hoy me senti muy bien fui por helado y luego comi pizza", IdCategoria = 3, Fecha = DateTime.Now});
            modelBuilder.Entity<Nota>().HasData(new Nota { Id = 2, Titulo = "Palanca", Descripcion = "Realizar 5 repeticiones con 40 segundos de descanso entre repeticiones", IdCategoria = 2, Fecha = DateTime.Now });
            modelBuilder.Entity<Nota>().HasData(new Nota { Id = 3, Titulo = "Barra", Descripcion = "Hacer 45 repeticiones minimo diario sobre la barra", IdCategoria = 2, Fecha = DateTime.Now });
            modelBuilder.Entity<Nota>().HasData(new Nota { Id = 4, Titulo = "Milanesa", Descripcion = "Pollo, panco, aceite, pimienta, chipotle, salsa de ajo, perejil", IdCategoria = 4, Fecha = DateTime.Now });
            modelBuilder.Entity<Nota>().HasData(new Nota { Id = 5, Titulo = "Pastas en salsa de champiñones", Descripcion = "Pastas, Pollo, crema de champiñones, pimenton, cebolla, pimienta, salsa, leche y crema de leche", IdCategoria = 4, Fecha = DateTime.Now });
            modelBuilder.Entity<Nota>().HasData(new Nota { Id = 6, Titulo = "Masa de pizza", Descripcion = "Arina, levadura, pasta de tomate", IdCategoria = 4, Fecha = DateTime.Now });
            modelBuilder.Entity<Nota>().HasData(new Nota { Id = 7, Titulo = "Migrar BD", Descripcion = "add-migration (nombredelamigracion; update-database;)", IdCategoria = 7, Fecha = DateTime.Now });

        }

    }
}