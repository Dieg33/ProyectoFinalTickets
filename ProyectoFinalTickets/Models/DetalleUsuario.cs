using System;
using System.ComponentModel.DataAnnotations;

namespace TuProyecto.Models
{
    public class DetalleUsuario
    {
        [Key]
        public int id_detalle { get; set; }

        [Required]
        public DateTime ultima_actividad { get; set; }

        [Required]
        [StringLength(50)]
        public string area_reqerida { get; set; }

        [Required]
        [StringLength(100)]
        public string direccion { get; set; }

        // No se necesita navegación inversa obligatoria aquí
    }
}
