using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuProyecto.Models
{
    public class Adjuntos
    {
        [Key]
        public int id_adjunto { get; set; }

        [ForeignKey(nameof(Ticket))]
        public int id_ticket { get; set; }

        [Required]
        [StringLength(100)]
        public string adjunto { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
