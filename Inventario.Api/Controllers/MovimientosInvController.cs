using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosInvController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public MovimientosInvController(InventarioDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientos()
        {
            var movimientos = await _context.MovimientosInv
                .AsNoTracking()
                .ToListAsync();
            return Ok(movimientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventario>> GetMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInv
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdMovimiento == id);

            if (movimiento == null) return NotFound();
            return Ok(movimiento);
        }

        [HttpPost]
        public async Task<ActionResult<MovimientoInventario>> CrearMovimiento(MovimientoInventario movimiento)
        {
            _context.MovimientosInv.Add(movimiento);
            await _context.SaveChangesAsync();
            return Ok(movimiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMovimiento(int id, MovimientoInventario movimiento)
        {
            if (id != movimiento.IdMovimiento) return BadRequest();
            _context.Entry(movimiento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInv.FindAsync(id);
            if (movimiento == null) return NotFound();
            _context.MovimientosInv.Remove(movimiento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
