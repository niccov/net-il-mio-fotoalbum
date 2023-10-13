using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Database
{
    public class FotoContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Categoria> Categorie { get; set; }
        public DbSet<Messaggio> Messaggi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-album;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
