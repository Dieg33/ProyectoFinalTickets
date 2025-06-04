using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Models
{
    public class Comentarios
    {
        [Key]
        public int id_comentario { get; set; }

        [ForeignKey(nameof(Ticket))]
        public int id_ticker { get; set; }

        [ForeignKey(nameof(Tecnico))]
        public int id_tecnico { get; set; }

        [Required]
        [StringLength(200)]
        public string comentario { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual Tecnico Tecnico { get; set; }
    }
}
