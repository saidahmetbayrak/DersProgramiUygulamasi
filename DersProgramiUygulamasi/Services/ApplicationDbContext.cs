
using Microsoft.EntityFrameworkCore;

namespace DersProgramiUygulamasi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ogretmen> Ogretmenler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<DersProgrami> DersProgramlari { get; set; }

        // Herhangi bir OnConfiguring methoduna gerek yok çünkü options artık DI'dan (dependency injection) gelecek
    }
}
