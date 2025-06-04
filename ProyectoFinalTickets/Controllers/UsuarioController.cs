using Microsoft.AspNetCore.Mvc;
using TuProyecto.Data;
using TuProyecto.Models;

namespace ProyectoFinalTickets.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("Registrarse");
        }


        public IActionResult Registrarse()
        {
            return View();

        }

        [HttpPost]
        public IActionResult UsuariosRegistrarse(string nombre,string apellidos,string empresa,string correo)
        {
            var nevoUsuario = new Usuario
            {
                nombre = nombre,
                apellidos = apellidos,
                empresa = empresa,
                correo = correo
            };
            _context.Usuarios.Add(nevoUsuario);
            _context.SaveChanges();
            return View();
        }
    }


    
   


}
