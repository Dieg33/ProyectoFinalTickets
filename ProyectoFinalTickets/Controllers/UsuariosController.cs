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

        [HttpGet]
        public IActionResult Tickets()
        {
            // Llenar el combo de prioridades
            ViewBag.Prioridades = new List<string> { "Alta", "Media", "Baja" };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Tickets(Ticket modelo, IFormFile ArchivoAdjunto)
        {
            if (ModelState.IsValid)
            {
                // Asignar la fecha de creación
                modelo.fecha_creacion = DateTime.Now;

                // Asignar estado inicial por defecto
                modelo.estado = "Abierto";

                // Procesar el archivo adjunto (si lo hay)
                if (ArchivoAdjunto != null && ArchivoAdjunto.Length > 0)
                {
                    // Ruta donde guardar (debes definir esta variable o usar wwwroot/archivos, por ejemplo)
                    var carpetaArchivos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos");
                    if (!Directory.Exists(carpetaArchivos))
                        Directory.CreateDirectory(carpetaArchivos);

                    var rutaArchivo = Path.Combine(carpetaArchivos, ArchivoAdjunto.FileName);

                    // Guardar el archivo
                    using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                    {
                        await ArchivoAdjunto.CopyToAsync(stream);
                    }

                    // Crear objeto adjunto
                    var adjunto = new Adjuntos
                    {
                      //  nombre_archivo = ArchivoAdjunto.FileName,
                        //ruta = "/archivos/" + ArchivoAdjunto.FileName, // para visualizar luego
                    };

                    // Inicializar lista si está nula
                    if (modelo.Adjuntos == null)
                        modelo.Adjuntos = new List<Adjuntos>();

                    modelo.Adjuntos.Add(adjunto);
                }

                // Guardar el ticket
                _context.Tickets.Add(modelo);
                await _context.SaveChangesAsync();

                return RedirectToAction("MisTickets");
            }

            ViewBag.Prioridades = new List<string> { "Alta", "Media", "Baja" };
            return View(modelo);
        }

        [HttpGet]
        public IActionResult MisTickets()
        {
            var tickets = new List<Ticket>
    {
        new Ticket { id_ticket = 1, asunto = "Prueba", fecha_creacion = DateTime.Now, estado = "Abierto", correo = "correo@ejemplo.com" }
    };

            return View(tickets);
        }




    }
}