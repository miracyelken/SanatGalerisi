using Microsoft.EntityFrameworkCore;

namespace SanatGalerisi.Models
{
    public class SanalSanatGalerisiDbContext:DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Anasayfa> Anasayfas { get; set; }
        public DbSet<Galeri> Galeris { get; set; }
        public DbSet<Nedir> Nedirs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=EREN;Initial Catalog= SanalSanatGalerisiDb;Integrated Security= true; TrustServerCertificate=true");
        }
    }
}
