using Microsoft.EntityFrameworkCore;
using PixAPI.Repository.Entities;

namespace PixAPI.Repository.Context
{
    public class ChavePixContext : DbContext
    {
        public ChavePixContext(DbContextOptions<ChavePixContext> options) 
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ChavePix> ChavePix { get; set; }
        public DbSet<UsuarioChavePix> UsuarioChavePix { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
