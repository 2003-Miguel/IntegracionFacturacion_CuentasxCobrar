using Microsoft.EntityFrameworkCore;
using FarmaciaCarolAPI.Models;
using System.Collections.Generic;
namespace FarmaciaCarolAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<CuentaPorCobrar> CuentasPorCobrar { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Factura>()
                .Property(f => f.FacturaID)
                .ValueGeneratedNever();
        }

    }

}
