using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string apellidos { get; set; }

        [Required]
        [StringLength(50)]
        public string empresa { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string correo { get; set; }

        // FK DetalleUsuario - Uno a uno
        [ForeignKey(nameof(DetalleUsuario))]
        public int id_detalle { get; set; }

        public virtual DetalleUsuario DetalleUsuario { get; set; }

        // Un Usuario puede tener muchas Notificaciones
        public virtual ICollection<Notificaciones> Notificaciones { get; set; }
    }
}
