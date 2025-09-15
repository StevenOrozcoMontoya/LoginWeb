using Microsoft.EntityFrameworkCore;
using LoginWeb.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoginWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
