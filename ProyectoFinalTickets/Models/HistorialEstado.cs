using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Models
{
    public class HistorialEstado
    {
        [Key]
        public int id_estados { get; set; }

        [ForeignKey(nameof(Ticket))]
        public int id_ticket { get; set; }

        [Required]
        [StringLength(50)]
        public string estado { get; set; }

        [Required]
        public DateTime fecha_actualizacion { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
