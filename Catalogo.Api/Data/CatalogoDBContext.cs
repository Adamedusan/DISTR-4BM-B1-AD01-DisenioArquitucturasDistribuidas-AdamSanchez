using Catalogo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Data
{
    public class CatalogoDBContext : DbContext
    {
        public CatalogoDBContext(DbContextOptions<CatalogoDBContext> options) : base(options) { }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Producto> Producto { get; set; }
    }
}