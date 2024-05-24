using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notas.Server.Models;

namespace Notas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly MiDbContext _context;

        public CategoriasController(MiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> AgregarCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpPost]
        [Route("CrearSinCategoria")]
        public async Task<IActionResult> CrearSinCategoria(string email)
        {
            var categoriaExistente = await _context.Categorias
                .Where(c => c.Nombre == "Sin Categoria" && c.Email == email)
                .FirstOrDefaultAsync();

            if (categoriaExistente != null)
            {
                return Ok("Ya existe una categoría 'Sin Categoria' asociada a este email.");
            }

            var nuevaCategoria = new Categoria
            {
                Nombre = "Sin Categoria",
                Email = email
            };

            _context.Categorias.Add(nuevaCategoria);
            await _context.SaveChangesAsync();

            return Ok("Se ha creado una nueva categoría 'Sin Categoria' para el email proporcionado.");
        }


        [HttpGet]
        [Route("ConsultarTodo")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> ConsultarCategoriasCompleta()
        {
            var categorias = await _context.Categorias.Include(c => c.Notas).ToListAsync();

            var categoriasDTO = categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Email = c.Email,
                Notas = c.Notas
            }).ToList();

            return Ok(categoriasDTO);
        }

        [HttpGet]
        [Route("Filtrar/CategoriaUsuario")]
        public async Task<ActionResult<IEnumerable<Categoria>>> FiltarPorUsuario(string email)
        {
            var categorias = await _context.Categorias.Include(c => c.Notas).Where(c => c.Email == email).ToListAsync();

            var categoriasDTO = categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Email = c.Email,
                Notas = c.Notas
                
            }).ToList();

            if (categorias == null || categorias.Count == 0)
            {
                return NotFound("No se encontraron categorias.");
            }
            else
            {

                return Ok(categorias);
            }
        }

        [HttpGet]
        [Route("Filtrar/Id")]
        public async Task<ActionResult> FiltarPorId(int id)
        {
            Categoria categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound("No existe la categoria que buscas");
            }

            return Ok(categoria);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> ActualizarCategoria(int id, Categoria categoria)
        {
            var categoriaExistente = await _context.Categorias.FindAsync(id);

            categoriaExistente!.Nombre = categoria.Nombre;

            await _context.SaveChangesAsync();

            return Ok("Se actualizo exitosamente");
        }

        //[HttpDelete]
        //[Route("Eliminar")]
        //public async Task<IActionResult> Eliminar(int id)
        //{
        //    var categoriaEliminar = await _context.Categorias.FindAsync(id);

        //    if (categoriaEliminar == null)
        //    {
        //        return NotFound("No existe la categoria que buscas");
        //    }
        //    if (categoriaEliminar.Nombre == "Sin Categoria")
        //    {
        //        return NotFound("La categoria que deseas eliminar es la categoria predeterminada");
        //    }

        //    var notasEnCategoria = await _context.Notas
        //                                        .Where(p => p.IdCategoria == id)
        //                                        .ToListAsync();

        //    if (notasEnCategoria.Any())
        //    {
        //        var categoriaNueva = await _context.Categorias.FindAsync(1);

        //        if (categoriaNueva != null)
        //        {
        //            foreach (var notas in notasEnCategoria)
        //            {
        //                notas.IdCategoria = categoriaNueva.Id;
        //            }
        //        }
        //    }

        //    _context.Categorias.Remove(categoriaEliminar);

        //    await _context.SaveChangesAsync();

        //    return Ok("Se elimino exitosamente la categoria");
        //}

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoriaEliminar = await _context.Categorias.FindAsync(id);

            if (categoriaEliminar == null)
            {
                return NotFound("No existe la categoria que buscas");
            }
            if (categoriaEliminar.Nombre == "Sin Categoria")
            {
                return NotFound("La categoria que deseas eliminar es la categoria predeterminada");
            }

            var categoriaSinCategoria = await _context.Categorias
                .Where(c => c.Nombre == "Sin Categoria" && c.Email == categoriaEliminar.Email)
                .FirstOrDefaultAsync();

            if (categoriaSinCategoria == null)
            {
                return NotFound("No se encontró una categoría 'Sin Categoria' asociada al email de la categoría que deseas eliminar");
            }

            var notasEnCategoria = await _context.Notas
                                                 .Where(p => p.IdCategoria == id)
                                                 .ToListAsync();

            if (notasEnCategoria.Any())
            {
                foreach (var nota in notasEnCategoria)
                {
                    nota.IdCategoria = categoriaSinCategoria.Id;
                }
            }

            _context.Categorias.Remove(categoriaEliminar);
            await _context.SaveChangesAsync();

            return Ok("Se eliminó exitosamente la categoría");
        }


    }
}
