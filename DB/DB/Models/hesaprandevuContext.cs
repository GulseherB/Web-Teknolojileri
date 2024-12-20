using Microsoft.EntityFrameworkCore;
namespace DB.Models
{
    public class hesaprandevuContext : DbContext
    {
        public DbSet<Hesap> Hesaplar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=kuafor;Trusted_Connection=True;");
        }
    }
}
