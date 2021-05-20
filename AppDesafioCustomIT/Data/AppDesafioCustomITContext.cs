using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppDesafioCustomIT.Models;

namespace AppDesafioCustomIT.Data
{
    public class AppDesafioCustomITContext : DbContext
    {
        public AppDesafioCustomITContext (DbContextOptions<AppDesafioCustomITContext> options)
            : base(options)
        {
        }

        public DbSet<AppDesafioCustomIT.Models.Telefone> Telefone { get; set; }

        public DbSet<AppDesafioCustomIT.Models.Pessoa> Pessoa { get; set; }
    }
}
