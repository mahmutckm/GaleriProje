using Microsoft.EntityFrameworkCore;
using ArabaGaleri.Models;

namespace ArabaGaleri.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Araba> Arabalar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Araba için varsayılan değerler
            modelBuilder.Entity<Araba>()
                .Property(a => a.OlusturmaTarihi)
                .HasDefaultValueSql("GETDATE()");

            // Kullanıcı için varsayılan değerler
            modelBuilder.Entity<Kullanici>()
                .Property(k => k.OlusturmaTarihi)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Kullanici>()
                .Property(k => k.IsAdmin)
                .HasDefaultValue(false);

            // Kullanıcı mail benzersiz olmalı
            modelBuilder.Entity<Kullanici>()
                .HasIndex(k => k.Email)
                .IsUnique();

            // Kullanıcıadı benzersiz olmalı
            modelBuilder.Entity<Kullanici>()
                .HasIndex(k => k.KullaniciAdi)
                .IsUnique();
        }
    }
}




