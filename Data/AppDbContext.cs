using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Reserva)
                .WithOne(r => r.Pago)
                .HasForeignKey<Pago>(p => p.ReservaId);

            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Admin" },
                new Rol { Id = 2, Nombre = "Usuario" }
            );

            modelBuilder.Entity<Servicio>().HasData(
                new Servicio
                {
                    Id = 1,
                    Nombre = "Corte de cabello",
                    Descripcion = "Servicio básico de corte de cabello",
                    Precio = 30
                },
                new Servicio
                {
                    Id = 2,
                    Nombre = "Consulta general",
                    Descripcion = "Reserva para consulta general",
                    Precio = 50
                }
            );
        }
    }
}