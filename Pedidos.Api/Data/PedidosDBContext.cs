using Microsoft.EntityFrameworkCore;
using Pedidos.Api.Models;
using PedidosModel = Pedidos.Api.Models.Pedido;

namespace Pedidos.Api.Data
{
    public class PedidosDBContext : DbContext
    {
        public PedidosDBContext(DbContextOptions<PedidosDBContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<PedidosModel> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
    }
}