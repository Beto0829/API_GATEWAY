using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notas.Server.Models;
using System.Globalization;

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
        public async Task<IActionResult> AgregarNota(NotaDTO notaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nota = new Nota
            {
                Titulo = notaDTO.Titulo,
                Descripcion = notaDTO.Descripcion,
                IdCategoria = notaDTO.IdCategoria,
                Fecha = DateTime.Now
            };

            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();

            return Ok("Se guardo exitosamente");
        }

        [HttpGet]
        [Route("Consultar")]
        public async Task<ActionResult<IEnumerable<Nota>>> Consultar()
        {
            var notas = await _context.Notas
                        .Select(n => new {
                           n.Id,
                           n.Titulo,
                           n.Descripcion,
                           n.IdCategoria,
                           Fecha = n.Fecha.ToString("dd/MM/yy/ HH:mm")
                        })
                        .ToListAsync();

            if (notas == null || notas.Count == 0)
            {
                return NotFound("No existen los datos que buscas");
            }
            else
            {
                return Ok(notas);
            }
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
        public async Task<IActionResult> ActualizarNota(int id, NotaDTO notaDTO)
        {
            var notaExistente = await _context.Notas.FindAsync(id);

            if (notaExistente == null)
            {
                return NotFound("No existe la nota que buscas");
            }
            else
            {
                notaExistente!.Titulo = notaDTO.Titulo;
                notaExistente!.Descripcion = notaDTO.Descripcion;
                notaExistente!.IdCategoria = notaDTO.IdCategoria;
                notaExistente!.Fecha = DateTime.Now;


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
