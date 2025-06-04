using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Models;
using TuProyecto.Data;
using TuProyecto.Models;

namespace ProyectoFinalTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdjuntosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdjuntosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Adjuntos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adjuntos>>> GetAdjuntos()
        {
            return await _context.Adjuntos
                .Include(a => a.Ticket)
                .ToListAsync();
        }

        // GET: api/Adjuntos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adjuntos>> GetAdjunto(int id)
        {
            var adjunto = await _context.Adjuntos
                .Include(a => a.Ticket)
                .FirstOrDefaultAsync(a => a.id_adjunto == id);

            if (adjunto == null)
            {
                return NotFound();
            }

            return adjunto;
        }

        // POST: api/Adjuntos
        [HttpPost]
        public async Task<ActionResult<Adjuntos>> PostAdjunto(Adjuntos adjunto)
        {
            _context.Adjuntos.Add(adjunto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdjunto), new { id = adjunto.id_adjunto }, adjunto);
        }

        // PUT: api/Adjuntos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdjunto(int id, Adjuntos adjunto)
        {
            if (id != adjunto.id_adjunto)
            {
                return BadRequest();
            }

            _context.Entry(adjunto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Adjuntos.Any(e => e.id_adjunto == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Adjuntos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdjunto(int id)
        {
            var adjunto = await _context.Adjuntos.FindAsync(id);
            if (adjunto == null)
            {
                return NotFound();
            }

            _context.Adjuntos.Remove(adjunto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
