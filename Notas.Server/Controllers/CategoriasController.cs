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

        //[HttpGet]
        //[Route("Consultar")]
        //public async Task<ActionResult<IEnumerable<Categoria>>> ConsultarCategorias()
        //{
        //    var categorias = await _context.Categorias.ToListAsync();

        //    return Ok(categorias);
        //}

        [HttpGet]
        [Route("ConsultarTodo")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> ConsultarCategoriasCompleta()
        {
            var categorias = await _context.Categorias.Include(c => c.Notas).ToListAsync();

            var categoriasDTO = categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Notas = c.Notas
            }).ToList();

            return Ok(categoriasDTO);
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

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoriaEliminar = await _context.Categorias.FindAsync(id);

            if (categoriaEliminar == null)
            {
                return NotFound("No existe la categoria que buscas");
            }
            if (categoriaEliminar.Id == 1)
            {
                return NotFound("La categoria que deseas eliminar es la categoria predeterminada");
            }

            var notasEnCategoria = await _context.Notas
                                                .Where(p => p.IdCategoria == id)
                                                .ToListAsync();

            if (notasEnCategoria.Any())
            {
                var categoriaNueva = await _context.Categorias.FindAsync(1);

                if (categoriaNueva != null)
                {
                    foreach (var notas in notasEnCategoria)
                    {
                        notas.IdCategoria = categoriaNueva.Id;
                    }
                }
            }

            _context.Categorias.Remove(categoriaEliminar);

            await _context.SaveChangesAsync();

            return Ok("Se elimino exitosamente la categoria");
        }

    }
}
