using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Api.Data;
using Pedidos.Api.Models;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly PedidosDBContext _context;

        public ClienteController(PedidosDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _context.Cliente
                .AsNoTracking()
                .ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Cliente
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CrearCliente(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente) return BadRequest();
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarCliente(int id)
        {
            var cliente = await _context.Cliente
                .Include(c => c.Pedidos)
                    .ThenInclude(p => p.Detalles)
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null) return NotFound();

            if (cliente.Pedidos != null && cliente.Pedidos.Any())
            {
                foreach (var pedido in cliente.Pedidos)
                {
                    if (pedido.Detalles != null && pedido.Detalles.Any())
                    {
                        _context.DetallePedido.RemoveRange(pedido.Detalles);
                    }
                }
                _context.Pedidos.RemoveRange(cliente.Pedidos);
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}