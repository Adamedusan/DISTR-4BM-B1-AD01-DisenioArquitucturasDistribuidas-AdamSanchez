using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Api.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        [Column("IdPedido")]
        public int IdPedido { get; set; }

        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Column("Fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Column("Total", TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Estado")]
        public string Estado { get; set; } = string.Empty;

        [ForeignKey(nameof(IdCliente))]
        public Cliente? Cliente { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}