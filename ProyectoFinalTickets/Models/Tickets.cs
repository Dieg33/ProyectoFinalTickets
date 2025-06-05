namespace ProyectoFinalTickets.Models
{
    public class Tickets
    {
        public int id_ticket { get; set; }
        public string asunto { get; set; }
        public DateTime fecha_creacion { get; set; }
        public string estado { get; set; }
        public string correo { get; set; }
    }
}
