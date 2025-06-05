using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalTickets.Models
{
    public class Categoria
    {
        [Key]
        public int id_categoria { get; set; }

        [Required]
        [StringLength(100)]
        public string categoria { get; set; }

        // Una categoría puede tener muchos tickets
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
