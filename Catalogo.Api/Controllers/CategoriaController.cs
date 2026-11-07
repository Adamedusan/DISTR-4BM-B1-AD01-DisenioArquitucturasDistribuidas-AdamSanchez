using Catalogo.Api.Data;
using Catalogo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CatalogoDBContext _context;

        public CategoriaController(CatalogoDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            var categorias = await _context.Categoria
                .AsNoTracking()
                .ToListAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categoria
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCategoria == id);

            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CrearCategoria(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
            return Ok(categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria) return BadRequest();
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null) return NotFound();
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}