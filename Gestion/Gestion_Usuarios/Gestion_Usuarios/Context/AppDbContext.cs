using Gestion_Usuarios.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Gestion_Usuarios.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}