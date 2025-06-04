using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Models;
using TuProyecto.Data;
using TuProyecto.Models;

namespace ProyectoFinalTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecnicoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TecnicoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tecnico>>> GetTecnicos()
        {
            return await _context.Tecnicos
                .Include(t => t.Tickets)
                .Include(t => t.Comentarios)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tecnico>> GetTecnico(int id)
        {
            var tecnico = await _context.Tecnicos
                .Include(t => t.Tickets)
                .Include(t => t.Comentarios)
                .FirstOrDefaultAsync(t => t.id_tecnico == id);

            if (tecnico == null)
                return NotFound();

            return tecnico;
        }

        [HttpPost]
        public async Task<ActionResult<Tecnico>> PostTecnico(Tecnico tecnico)
        {
            _context.Tecnicos.Add(tecnico);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTecnico), new { id = tecnico.id_tecnico }, tecnico);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTecnico(int id, Tecnico tecnico)
        {
            if (id != tecnico.id_tecnico)
                return BadRequest();

            _context.Entry(tecnico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tecnicos.Any(e => e.id_tecnico == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTecnico(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico == null)
                return NotFound();

            _context.Tecnicos.Remove(tecnico);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

