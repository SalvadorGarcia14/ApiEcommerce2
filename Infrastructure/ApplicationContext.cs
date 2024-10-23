using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Orden> Ordenes { get; set; } = null!;
    }
}