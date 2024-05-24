using Microsoft.EntityFrameworkCore;
using Notas.Server.Models;

namespace Notas.Server
{
    public class Start
    {
        private MiDbContext _context { get; set; }
        public Start(DbContext context)
        {
            _context = (MiDbContext)context;
        }

        public async Task Seed()
        {
            try
            {
                if (!_context.Categorias.Any()) await CrearDatosIniciales();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task CrearDatosIniciales()
        {
            var categorias = new List<Categoria>
        {
            new Categoria
            {
                Nombre = "Deportes",
                Email = "admin@gmail.com"
            },
             new Categoria
            {
                Nombre = "UPC",
                 Email = "admin@gmail.com"
            },
              new Categoria
            {
                Nombre = "GYM",
                 Email = "admin@gmail.com"
            },
        };

            await _context.AddRangeAsync(categorias);
            await _context.SaveChangesAsync();
        }
    }
}
