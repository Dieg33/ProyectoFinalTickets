using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
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

        public IActionResult Login()
        {
            return View("Login");
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
        public async Task<IActionResult> UsuariosRegistrarse(Usuario usuario)
        {
            // Validar campos requeridos manualmente (por si no usas DataAnnotations)
            if (string.IsNullOrWhiteSpace(usuario.nombre) || string.IsNullOrWhiteSpace(usuario.apellidos) ||
                string.IsNullOrWhiteSpace(usuario.empresa) || string.IsNullOrWhiteSpace(usuario.correo) ||
                string.IsNullOrWhiteSpace(usuario.contraseña))
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios.");
                return View("Registrarse", usuario);
            }

            // Verificar si ya existe el correo
            var existeCorreo = await _context.Usuario.FirstOrDefaultAsync(u => u.correo == usuario.correo);
            if (existeCorreo != null)
            {
                ModelState.AddModelError("", "Este correo ya está registrado.");
                return View("Registrarse", usuario);
            }

            try
            {
                usuario.rol = "cliente"; // Puedes establecerlo aquí si no se estableció antes

                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("RegistroExitoso");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar usuario: " + ex.Message);
                return View("Registrarse", usuario);
            }
        }




        public IActionResult PrincipalCliente()
        {
            return View();
        }

       
        }



    }

