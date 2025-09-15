using LoginWeb.Data;
using Microsoft.AspNetCore.Mvc;
using LoginWeb.Data;
using LoginWeb.Models;

namespace LoginWeb.Controllers
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
            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }
    }
}
