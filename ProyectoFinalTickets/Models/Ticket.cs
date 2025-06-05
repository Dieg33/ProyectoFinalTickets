using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalTickets.Models
{
    
    public class Ticket
    {
        [Key]
        public int id_ticket { get; set; }

        [Required]
        [StringLength(100)]
        public string asunto { get; set; }

        [Required]
        [StringLength(200)]
        public string descripcion { get; set; }

        [Required]
        public DateTime fecha_creacion { get; set; }

        [ForeignKey(nameof(Categoria))]
        public int id_categoria { get; set; }

        [Required]
        [StringLength(50)]
        public string prioridad { get; set; }

        [Required]
        [StringLength(50)]
        public string estado { get; set; }

        public string nombre { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }


        // FK a Tecnico (Opcional? Según diagrama)
        [ForeignKey(nameof(Tecnico))]
        public int? id_tecnico { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Tecnico Tecnico { get; set; }

        // Relación uno a muchos
        public virtual ICollection<Notificaciones> Notificaciones { get; set; }
        public virtual ICollection<HistorialEstado> HistorialEstados { get; set; }
        public virtual ICollection<Comentarios> Comentarios { get; set; }
        public virtual ICollection<Adjuntos> Adjuntos { get; set; }
    }
}
