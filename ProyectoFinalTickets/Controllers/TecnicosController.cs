using Microsoft.AspNetCore.Mvc;
using ProyectoFinalTickets.Data;
using ProyectoFinalTickets.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinalTickets.Controllers
{
    public class TecnicosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TecnicosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tecnico>>> GetTecnicos()
        {
            return await _context.Tecnicos
                .Include(t => t.Tickets)
                .Include(t => t.Comentarios)
                .ToListAsync();
        }

        public IActionResult portada()
        {
            return View();
        }
        public IActionResult ticketAsignado()
        {
            return View();
        }
    }
}
