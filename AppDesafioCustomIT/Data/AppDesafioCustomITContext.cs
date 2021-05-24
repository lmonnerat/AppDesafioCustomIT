using Microsoft.EntityFrameworkCore;

namespace AppDesafioCustomIT.Data
{
    public class AppDesafioCustomITContext : DbContext
    {
        public AppDesafioCustomITContext(DbContextOptions<AppDesafioCustomITContext> options)
            : base(options)
        {
        }

        public DbSet<AppDesafioCustomIT.Models.Telefone> Telefone { get; set; }

        public DbSet<AppDesafioCustomIT.Models.Pessoa> Pessoa { get; set; }
    }
}
