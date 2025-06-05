using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Models;
using ProyectoFinalTickets.Data;


namespace ProyectoFinalTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotificacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificaciones>>> GetNotificaciones()
        {
            return await _context.Notificaciones
                .Include(n => n.Ticket)
                .Include(n => n.Usuario)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notificaciones>> GetNotificacion(int id)
        {
            var notificacion = await _context.Notificaciones
                .Include(n => n.Ticket)
                .Include(n => n.Usuario)
                .FirstOrDefaultAsync(n => n.id_notificacion == id);

            if (notificacion == null)
                return NotFound();

            return notificacion;
        }

        [HttpPost]
        public async Task<ActionResult<Notificaciones>> PostNotificacion(Notificaciones notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotificacion), new { id = notificacion.id_notificacion }, notificacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificacion(int id, Notificaciones notificacion)
        {
            if (id != notificacion.id_notificacion)
                return BadRequest();

            _context.Entry(notificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Notificaciones.Any(n => n.id_notificacion == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound();

            _context.Notificaciones.Remove(notificacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
