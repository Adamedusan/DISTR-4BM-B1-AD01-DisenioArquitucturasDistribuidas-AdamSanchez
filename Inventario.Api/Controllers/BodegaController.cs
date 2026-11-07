using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public BodegaController(InventarioDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bodega>>> GetBodegas()
        {
            var bodegas = await _context.Bodega
                .AsNoTracking()
                .ToListAsync();
            return Ok(bodegas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bodega>> GetBodega(int id)
        {
            var bodega = await _context.Bodega
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.IdBodega == id);

            if (bodega == null) return NotFound();
            return Ok(bodega);
        }

        [HttpPost]
        public async Task<ActionResult<Bodega>> CrearBodega(Bodega bodega)
        {
            _context.Bodega.Add(bodega);
            await _context.SaveChangesAsync();
            return Ok(bodega);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarBodega(int id, Bodega bodega)
        {
            if (id != bodega.IdBodega) return BadRequest();
            _context.Entry(bodega).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarBodega(int id)
        {
            var bodega = await _context.Bodega.FindAsync(id);
            if (bodega == null) return NotFound();
            _context.Bodega.Remove(bodega);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}