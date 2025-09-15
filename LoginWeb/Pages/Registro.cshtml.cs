using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoginWeb.Data;
using LoginWeb.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

namespace LoginWeb.Pages
{
    public class RegistroModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegistroModel> _logger;

        public RegistroModel(ApplicationDbContext context, ILogger<RegistroModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string Nombre { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string RePassword { get; set; }

        public string Mensaje { get; set; }
        public bool RegistroExitoso { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(RePassword))
            {
                Mensaje = "Por favor, complete todos los campos.";
                return Page();
            }

            if (Password != RePassword)
            {
                Mensaje = "Las contraseñas no coinciden.";
                return Page();
            }

            // Verificar si el usuario ya existe
            var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.name == Nombre);
            if (usuarioExistente != null)
            {
                Mensaje = "Usuario actualmente registrado.";
                return Page();
            }

            // Registrar nuevo usuario
            var usuario = new Usuario
            {
                name = Nombre,
                Password = Password
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            Mensaje = "Registro correcto! Redirigiendo a la tabla en 5 segundos...";
            RegistroExitoso = true;

            return Page();
        }
    }
}
