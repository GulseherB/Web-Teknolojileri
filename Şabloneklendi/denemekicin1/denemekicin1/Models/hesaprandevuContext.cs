using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace denemekicin1.Models
{
    public class hesaprandevuContext : DbContext
    {
        public DbSet<Hesap> Hesaplar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; } // Yeni çalışan tablosu

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=yeni;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Randevu - Hesap ilişkisi
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Hesap)
                .WithMany(h => h.Randevular)
                .HasForeignKey(r => r.HesapID);

            // Randevu - Çalışan ilişkisi
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.Calisan)
                .WithMany(c => c.Randevular)
                .HasForeignKey(r => r.CalisanID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
