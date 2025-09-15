using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoginWeb.Data;
using LoginWeb.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb.Pages
{
    public class EliminarModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EliminarModel> _logger;

        public EliminarModel(ApplicationDbContext context, ILogger<EliminarModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string Nombre { get; set; }

        public string Mensaje { get; set; }
        public bool EliminacionExitosa { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                Mensaje = "Por favor ingrese el nombre del usuario.";
                return Page();
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.name == Nombre);
            if (usuario == null)
            {
                Mensaje = "Usuario no existe.";
                return Page();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            Mensaje = "Usuario eliminado! Redirigiendo a la tabla en 5 segundos...";
            EliminacionExitosa = true;

            return Page();
        }
    }
}
