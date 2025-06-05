using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Models;
using ProyectoFinalTickets.Data;


namespace ProyectoFinalTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentarios>>> GetComentarios()
        {
            return await _context.Comentarios
                .Include(c => c.Ticket)
                .Include(c => c.Tecnico)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comentarios>> GetComentario(int id)
        {
            var comentario = await _context.Comentarios
                .Include(c => c.Ticket)
                .Include(c => c.Tecnico)
                .FirstOrDefaultAsync(c => c.id_comentario == id);

            if (comentario == null)
                return NotFound();

            return comentario;
        }

        [HttpPost]
        public async Task<ActionResult<Comentarios>> PostComentario(Comentarios comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetComentario), new { id = comentario.id_comentario }, comentario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, Comentarios comentario)
        {
            if (id != comentario.id_comentario)
                return BadRequest();

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Comentarios.Any(e => e.id_comentario == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
                return NotFound();

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
