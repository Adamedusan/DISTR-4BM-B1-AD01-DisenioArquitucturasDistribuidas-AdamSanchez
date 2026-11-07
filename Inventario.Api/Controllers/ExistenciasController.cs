using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistenciasController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public ExistenciasController(InventarioDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Existencia>>> GetExistencias()
        {
            var existencias = await _context.Existencias
                .AsNoTracking()
                .ToListAsync();
            return Ok(existencias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Existencia>> GetExistencia(int id)
        {
            var existencia = await _context.Existencias
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.IdExistencia == id);

            if (existencia == null) return NotFound();
            return Ok(existencia);
        }

        [HttpPost]
        public async Task<ActionResult<Existencia>> CrearExistencia(Existencia existencia)
        {
            _context.Existencias.Add(existencia);
            await _context.SaveChangesAsync();
            return Ok(existencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarExistencia(int id, Existencia existencia)
        {
            if (id != existencia.IdExistencia) return BadRequest();
            _context.Entry(existencia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarExistencia(int id)
        {
            var existencia = await _context.Existencias.FindAsync(id);
            if (existencia == null) return NotFound();
            _context.Existencias.Remove(existencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}