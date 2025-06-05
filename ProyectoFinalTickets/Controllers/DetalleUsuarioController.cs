using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Models;
using ProyectoFinalTickets.Data;


namespace ProyectoFinalTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleUsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DetalleUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DetalleUsuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleUsuario>>> GetDetalleUsuarios()
        {
            return await _context.DetalleUsuarios.ToListAsync();
        }

        // GET: api/DetalleUsuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleUsuario>> GetDetalleUsuario(int id)
        {
            var detalle = await _context.DetalleUsuarios.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }

        // POST: api/DetalleUsuario
        [HttpPost]
        public async Task<ActionResult<DetalleUsuario>> PostDetalleUsuario(DetalleUsuario detalle)
        {
            _context.DetalleUsuarios.Add(detalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalleUsuario), new { id = detalle.id_detalle }, detalle);
        }

        // PUT: api/DetalleUsuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleUsuario(int id, DetalleUsuario detalle)
        {
            if (id != detalle.id_detalle)
            {
                return BadRequest();
            }

            _context.Entry(detalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DetalleUsuarios.Any(e => e.id_detalle == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/DetalleUsuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleUsuario(int id)
        {
            var detalle = await _context.DetalleUsuarios.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }

            _context.DetalleUsuarios.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

