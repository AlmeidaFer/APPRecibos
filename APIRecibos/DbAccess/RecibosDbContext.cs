using Entidades.Data;
using Entidades.Models;
using Microsoft.EntityFrameworkCore;

namespace APIRecibos.DbAccess
{
    public class RecibosDbContext : DbContext
    { 
        public RecibosDbContext(DbContextOptions<RecibosDbContext> options) : base(options)
        {
        }

        public DbSet<EUser> Users { get; set; }
        public DbSet<EProveedor> Proveedores { get; set; }
        public DbSet<EMoneda> Monedas { get; set; }
        public DbSet<ERecibos> Recibos { get; set; }
    }
}
