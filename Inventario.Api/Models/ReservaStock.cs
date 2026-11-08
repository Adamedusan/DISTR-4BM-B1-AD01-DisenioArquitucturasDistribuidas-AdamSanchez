using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventario.Api.Models
{
    [Table("ReservaStock")]
    public class ReservaStock
    {
        [Key]
        [Column("IdReserva")]
        public int IdReserva { get; set; }

        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Estado")]
        public string Estado { get; set; } = string.Empty;

        [ForeignKey(nameof(IdBodega))]
        [JsonIgnore]
        public Bodega? Bodega { get; set; }
    }
}
