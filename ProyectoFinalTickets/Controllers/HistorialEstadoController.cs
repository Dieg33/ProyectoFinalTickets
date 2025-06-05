using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Models;
using ProyectoFinalTickets.Data;


namespace ProyectoFinalTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialEstadoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistorialEstadoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialEstado>>> GetHistorialEstados()
        {
            return await _context.HistorialEstados
                .Include(h => h.Ticket)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialEstado>> GetHistorialEstado(int id)
        {
            var historial = await _context.HistorialEstados
                .Include(h => h.Ticket)
                .FirstOrDefaultAsync(h => h.id_estados == id);

            if (historial == null)
                return NotFound();

            return historial;
        }

        [HttpPost]
        public async Task<ActionResult<HistorialEstado>> PostHistorialEstado(HistorialEstado historialEstado)
        {
            _context.HistorialEstados.Add(historialEstado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHistorialEstado), new { id = historialEstado.id_estados }, historialEstado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialEstado(int id, HistorialEstado historialEstado)
        {
            if (id != historialEstado.id_estados)
                return BadRequest();

            _context.Entry(historialEstado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.HistorialEstados.Any(e => e.id_estados == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialEstado(int id)
        {
            var historial = await _context.HistorialEstados.FindAsync(id);
            if (historial == null)
                return NotFound();

            _context.HistorialEstados.Remove(historial);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
