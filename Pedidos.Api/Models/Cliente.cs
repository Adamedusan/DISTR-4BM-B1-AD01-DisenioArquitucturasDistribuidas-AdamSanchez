using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pedidos.Api.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(150)]
        [Column("Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("Cedula")]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        [Column("Correo")]
        public string Correo { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
