using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LoginWeb.Models;
using LoginWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace LoginWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }   

        public IList<Usuario> Usuarios { get; set; } = new List<Usuario>();
        [BindProperty]
        public string Nombre { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string LoginMensaje { get; set; }

        // Se ejecuta al cargar la página
        public async Task OnGetAsync()
        {
            Usuarios = await _context.Usuarios.ToListAsync();
        }

        // Se ejecuta cuando se envía el formulario
        public async Task<IActionResult> OnPostAsync()
        {
            Usuarios = await _context.Usuarios.ToListAsync();

            var usuarioValido = Usuarios.FirstOrDefault(u =>
                u.name == Nombre && u.Password == Password);

            if (usuarioValido != null)
            {
                LoginMensaje = $"Bienvenido, {usuarioValido.name}!";
            }
            else
            {
                LoginMensaje = "Usuario o contraseña incorrectos.";
            }

            return Page();
        }
    }
}
