﻿using Microsoft.AspNetCore.Mvc;
using TuProyecto.Data;
using Microsoft.EntityFrameworkCore;


namespace ProyectoFinalTickets.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController (ApplicationDbContext context)
        {

            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index(string nombre, string  contraseña)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(contraseña))
            {
                ViewBag.Error = "Debe ingresar correo y contraseña.";
                return View();
            }

            nombre = nombre?.Trim().ToLower();
            contraseña = contraseña?.Trim();

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u =>
                    u.contraseña.ToLower() == contraseña &&
                    u.contraseña == contraseña);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            if (!string.IsNullOrEmpty(usuario.rol))
            {
                if (usuario.rol.ToLower() == "admi")
                {
                    return RedirectToAction("Dashboard", "Admi");
                }
                else if (usuario.rol.ToLower() == "cliente")
                {
                    return RedirectToAction("PrincipalCliente", "Usuario");
                }
                else if (usuario.rol.ToLower() == "tecnico")
                {
                    return RedirectToAction("GestionTickets", "Tecnico"); 
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
        // Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
