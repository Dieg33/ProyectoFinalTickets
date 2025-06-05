using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalTickets.Data;

namespace ProyectoFinalTickets.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _Context;
        public LoginController(ApplicationDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string nombre, string contraseña)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contraseña))
            {
                ViewBag.Error = "Debe ingresar nombre y contraseña.";
                return View();
            }

            nombre = nombre.Trim().ToLower();
            contraseña = contraseña.Trim();

            var usuario = await _Context.Usuario
                .FirstOrDefaultAsync(u => u.nombre.ToLower() == nombre);

            if (usuario == null)
            {
                ViewBag.Error = "Nombre o contraseña incorrectos.";
                return View();
            }


            // Guardar datos en sesión
            HttpContext.Session.SetString("UserNombre", usuario.nombre);
            HttpContext.Session.SetString("UserRole", usuario.rol);

            // Redirigir según el rol
            if (!string.IsNullOrEmpty(usuario.rol))
            {
                if (usuario.rol.ToLower() == "admi")
                {
                    return RedirectToAction("Dashboard", "Admi");
                }
                else if (usuario.rol.ToLower() == "cliente")
                {
                    return RedirectToAction("Principal", "Usuarios");
                }
                else
                {
                    ViewBag.Error = "Rol desconocido.";
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "El rol del usuario no está definido.";
                return View();
            }
        }
    }
}
