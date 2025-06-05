using Microsoft.AspNetCore.Mvc;
using ProyectoFinalTickets.Data;
using ProyectoFinalTickets.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinalTickets.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(string nombre, string apellidos, string empresa, string correo, string contraseña, string rol)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya está registrado
                var usuarioExistente = await _context.Usuario
                                                     .FirstOrDefaultAsync(u => u.correo == correo);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError(string.Empty, "El correo ya está registrado.");
                    return View();
                }

                // Encriptar la contraseña
                var passwordHasher = new PasswordHasher<Usuario>();
                var hashedPassword = passwordHasher.HashPassword(null, contraseña);

                // Obtener o asignar un detalle de usuario predeterminado
                var detallePredeterminado = _context.DetalleUsuario; // Puede ser NULL o un detalle predeterminado

                // Crear el nuevo usuario
                var nuevoUsuario = new Usuario
                {
                    nombre = nombre,
                    apellidos = apellidos,
                    empresa = empresa,
                    correo = correo,
                    contraseña = hashedPassword,  // Contraseña encriptada
                    rol = rol ?? "cliente",  // Asignar 'cliente' por defecto si no se especifica rol
                   
                };

                // Guardar el nuevo usuario en la base de datos
                _context.Usuario.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                // Redirige al usuario a la página de inicio o a donde sea necesario
                return RedirectToAction("Index");
            }

            // Si hay errores, vuelve al formulario de registro
            return View();
        }
        [HttpGet]
        public IActionResult Principal()
        {
            return View();
        }


        public IActionResult Tickets()
        {
            return View();
        }

    }
}