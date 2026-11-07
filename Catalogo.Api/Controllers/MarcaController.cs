using Catalogo.Api.Data;
using Catalogo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly CatalogoDBContext _context;

        public MarcaController(CatalogoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            var marcas = await _context.Marca
                .AsNoTracking()
                .ToListAsync();
            return Ok(marcas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetMarca(int id)
        {
            var marca = await _context.Marca
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdMarca == id);

            if (marca == null) return NotFound();
            return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult<Marca>> CrearMarca(Marca marca)
        {
            _context.Marca.Add(marca);
            await _context.SaveChangesAsync();
            return Ok(marca);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMarca(int id, Marca marca)
        {
            if (id != marca.IdMarca) return BadRequest();
            _context.Entry(marca).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarMarca(int id)
        {
            var marca = await _context.Marca.FindAsync(id);
            if (marca == null) return NotFound();
            _context.Marca.Remove(marca);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}