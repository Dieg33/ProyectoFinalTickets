using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalTickets.Models
{
    public class Tecnico
    {
        [Key]
        public int id_tecnico { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string especialidad { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        [StringLength(100)]
        public string telefono { get; set; }

        // Un técnico puede tener varios Comentarios
        public virtual ICollection<Comentarios> Comentarios { get; set; }

        // Un técnico puede tener varios Tickets asignados? 
        // En el diagrama parece que Ticket tiene FK a Tecnico, no al revés, 
        // pero el diagrama muestra Ticket -> Tecnico como 1 a muchos?
        // Para reflejar esto, agrego la colección de Tickets para claridad:

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
