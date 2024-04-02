using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notas.Server.Models;

namespace Notas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : ControllerBase
    {
        private readonly MiDbContext _context;

        public NotasController(MiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> AgregarNota(Nota nota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpGet]
        [Route("Consultar")]
        public async Task<ActionResult<IEnumerable<Nota>>> ConsultarNota()
        {
            var notas = await _context.Notas.ToListAsync();

            return Ok(notas);
        }

        [HttpGet]
        [Route("Filtrar/Id")]
        public async Task<ActionResult> FiltarPorId(int id)
        {
            Nota nota = await _context.Notas.FindAsync(id);

            if (nota == null)
            {
                return NotFound("No existe la nota que buscas");
            }

            return Ok(nota);
        }

        [HttpGet]
        [Route("Filtrar/IdCategoria")]
        public async Task<ActionResult<List<Nota>>> FiltarPorIdCategoria(int idCategoria)
        {
            // Buscar todas las notas que tengan el IdCategoria especificado
            var notas = await _context.Notas
                .Where(n => n.IdCategoria == idCategoria)
                .ToListAsync();

            if (notas == null || notas.Count == 0)
            {
                return NotFound("No se encontraron notas para la categoría especificada.");
            }

            return Ok(notas);
        }


        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> ActualizarNota(int id, Nota nota)
        {
            var notaExistente = await _context.Notas.FindAsync(id);

            if (notaExistente == null)
            {
                return NotFound("No existe la nota que buscas");
            }
            else
            {
                notaExistente!.Titulo = nota.Titulo;
                notaExistente!.Descripcion = nota.Descripcion;
                notaExistente!.IdCategoria = nota.IdCategoria;


                await _context.SaveChangesAsync();

                return Ok("Se actualizo exitosamente");
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> EliminarNotas(int id)
        {
            var notaEliminar = await _context.Notas.FindAsync(id);

            if (notaEliminar == null)
            {
                return NotFound("No existe la nota que buscas");
            }
            else
            {
                _context.Notas.Remove(notaEliminar!);

                await _context.SaveChangesAsync();

                return Ok("Se elimino exitosamente");
            }
        }
    }
}
