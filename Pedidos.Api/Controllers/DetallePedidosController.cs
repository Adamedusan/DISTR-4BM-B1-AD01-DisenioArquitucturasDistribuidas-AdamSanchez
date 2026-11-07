using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Api.Data;
using Pedidos.Api.Models;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidosController : ControllerBase
    {
        private readonly PedidosDBContext _context;

        public DetallePedidosController(PedidosDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetalles()
        {
            var detalles = await _context.DetallePedido
                .AsNoTracking()
                .ToListAsync();
            return Ok(detalles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> GetDetalle(int id)
        {
            var detalle = await _context.DetallePedido
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.IdDetalle == id);

            if (detalle == null) return NotFound();
            return Ok(detalle);
        }

        [HttpPost]
        public async Task<ActionResult<DetallePedido>> CrearDetalle(DetallePedido detalle)
        {
            _context.DetallePedido.Add(detalle);
            await _context.SaveChangesAsync();
            return Ok(detalle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDetalle(int id, DetallePedido detalle)
        {
            if (id != detalle.IdDetalle) return BadRequest();
            _context.Entry(detalle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarDetalle(int id)
        {
            var detalle = await _context.DetallePedido.FindAsync(id);
            if (detalle == null) return NotFound();
            _context.DetallePedido.Remove(detalle);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}