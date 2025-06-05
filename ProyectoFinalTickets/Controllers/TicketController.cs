using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Models;
using ProyectoFinalTickets.Data;
using System.Threading.Tasks;
using System.Linq;

namespace ProyectoFinalTickets.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para obtener todos los tickets
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Ticket
                .Include(t => t.Categoria)
                .Include(t => t.Tecnico)
                .Include(t => t.Comentarios)
                .Include(t => t.Notificaciones)
                .Include(t => t.HistorialEstados)
                .ToListAsync();

            return View(tickets); // Retorna la vista con los tickets
        }

        // Acción para obtener un ticket específico por ID
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _context.Ticket
                .Include(t => t.Categoria)
                .Include(t => t.Tecnico)
                .Include(t => t.Comentarios)
                .Include(t => t.Notificaciones)
                .Include(t => t.HistorialEstados)
                .FirstOrDefaultAsync(t => t.id_ticket == id);

            if (ticket == null)
                return NotFound();

            return View(ticket); // Devuelve la vista de detalles de un ticket
        }

        // Acción para mostrar el formulario de creación de un nuevo ticket
        public IActionResult Create()
        {
            return View(); // Devuelve la vista de creación
        }

        // Acción para procesar la creación del nuevo ticket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Ticket.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirige a la lista de tickets
            }
            return View(ticket); // Si hay errores, regresa al formulario
        }

        // Acción para mostrar el formulario de edición de un ticket
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return View(ticket); // Devuelve la vista de edición con el ticket
        }

        // Acción para procesar la actualización de un ticket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.id_ticket)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(ticket).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ticket.Any(e => e.id_ticket == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index)); // Redirige a la lista de tickets
            }
            return View(ticket); // Si hay errores, regresa al formulario de edición
        }

        // Acción para confirmar la eliminación de un ticket
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(t => t.id_ticket == id);
            if (ticket == null)
                return NotFound();

            return View(ticket); // Devuelve la vista de confirmación de eliminación
        }

        // Acción para procesar la eliminación de un ticket
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Redirige a la lista de tickets
        }
    }
}
