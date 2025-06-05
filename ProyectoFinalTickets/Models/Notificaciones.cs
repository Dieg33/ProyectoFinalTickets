using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalTickets.Models
{
    public class Notificaciones
    {
        [Key]
        public int id_notificacion { get; set; }

        [ForeignKey(nameof(Ticket))]
        public int id_ticket { get; set; }

        [ForeignKey(nameof(Usuario))]
        public int id_usuario { get; set; }

        [Required]
        [StringLength(100)]
        public string mensaje { get; set; }

        [Required]
        public DateTime fecha_envio { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
