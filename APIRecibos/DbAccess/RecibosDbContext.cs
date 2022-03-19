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
    }
}
