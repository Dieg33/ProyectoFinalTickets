using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalTickets.Models
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

        [Required]
        public string nombre_archivo { get; set; }

        public string ruta { get; set; } // Ruta donde está almacenado el archivo

        public virtual Ticket Ticket { get; set; }
    
    }
}
