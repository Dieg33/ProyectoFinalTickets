using Microsoft.AspNetCore.Mvc;
using ProyectoFinalTickets.Services;

namespace ProyectoFinalTickets.Controllers
{
    public class CorreoController : Controller
    {
        private readonly CorreoService _correoService;

        public CorreoController(CorreoService correoService)
        {
            _correoService = correoService;
        }

        public IActionResult EnviarPrueba()
        {
            var resultado = _correoService.Enviar("kevin.vides1@catolica.edu.sv", "Prueba de correo", "Este es un mensaje de prueba desde la app.");
            return Content(resultado);
        }

    }
}
