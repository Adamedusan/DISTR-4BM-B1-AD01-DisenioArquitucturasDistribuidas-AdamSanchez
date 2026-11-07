using Inventario.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Api.Models
{
    [Table("Bodega")]
    public class Bodega
    {
        [Key]
        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("Ciudad")]
        public string Ciudad { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column("Direccion")]
        public string Direccion { get; set; } = string.Empty;

        public ICollection<Existencia> Existencias { get; set; } = new List<Existencia>();
        public ICollection<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();
        public ICollection<ReservaStock> Reservas { get; set; } = new List<ReservaStock>();
    }
}

