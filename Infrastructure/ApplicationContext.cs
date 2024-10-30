using Domain.Entities;
using Domain.Entities.Domain.Entities;
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
        public DbSet<DetalleOrden> DetallesOrden { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación uno a muchos entre Orden y DetalleOrden
            modelBuilder.Entity<Orden>()
                .HasMany(o => o.DetallesOrden)
                .WithOne(d => d.Orden)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}