using Microsoft.EntityFrameworkCore;
using PixAPI.Repository.Entities;

namespace PixAPI.Repository.Context
{
    public class PixAPIContext : DbContext
    {
        public PixAPIContext(DbContextOptions<PixAPIContext> options) 
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
