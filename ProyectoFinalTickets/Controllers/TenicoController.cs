using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Data; // Asegúrate de tener el namespace correcto
using ProyectoFinalTickets.Models; // Ajusta según tu estructura
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalTickets.Controllers
{
    public class TecnicoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TecnicoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔵 Acción para mostrar vista del panel técnico
        public IActionResult principal()
        {
            return View();
        }

        // 🔵 API RESTful - Obtener todos los técnicos
        [HttpGet]
        [Route("api/tecnicos")]
        public async Task<ActionResult<IEnumerable<Tecnico>>> GetTecnicos()
        {
            return await _context.Tecnicos
                .Include(t => t.Tickets)
                .Include(t => t.Comentarios)
                .ToListAsync();
        }

        // 🔵 API RESTful - Obtener técnico por ID
        [HttpGet("api/tecnicos/{id}")]
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

        // 🔵 API RESTful - Crear nuevo técnico
        [HttpPost("api/tecnicos")]
        public async Task<ActionResult<Tecnico>> PostTecnico([FromBody] Tecnico tecnico)
        {
            _context.Tecnicos.Add(tecnico);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTecnico), new { id = tecnico.id_tecnico }, tecnico);
        }

        // 🔵 API RESTful - Actualizar técnico
        [HttpPut("api/tecnicos/{id}")]
        public async Task<IActionResult> PutTecnico(int id, [FromBody] Tecnico tecnico)
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

        // 🔵 API RESTful - Eliminar técnico
        [HttpDelete("api/tecnicos/{id}")]
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

