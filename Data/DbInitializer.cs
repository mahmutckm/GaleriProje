using ArabaGaleri.Models;
using ArabaGaleri.Helpers;

namespace ArabaGaleri.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Kullanicilar.Any())
            {
                return;
            }

            var admin = new Kullanici
            {
                KullaniciAdi = "admin",
                Email = "admin@araba.com",
                Sifre = PasswordHasher.HashPassword("admin123"),
                IsAdmin = true
            };

            context.Kullanicilar.Add(admin);

            var arabalar = new Araba[]
            {
                new Araba
                {
                    Marka = "BMW",
                    Model = "320i",
                    Yil = 2020,
                    Fiyat = 850000,
                    Kilometre = 25000,
                    YakıtTipi = "Benzin",
                    VitesTipi = "Otomatik",
                    Renk = "Beyaz",
                    Aciklama = "Tek kullanıcılı, bakımlı, garanti içinde.",
                    ResimUrl = "https://images.unsplash.com/photo-1555215695-3004980ad54e?w=800",
                    SatildiMi = false
                },
                new Araba
                {
                    Marka = "Mercedes-Benz",
                    Model = "C200",
                    Yil = 2019,
                    Fiyat = 920000,
                    Kilometre = 35000,
                    YakıtTipi = "Benzin",
                    VitesTipi = "Otomatik",
                    Renk = "Siyah",
                    Aciklama = "Premium paket, tam donanımlı.",
                    ResimUrl = "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=800",
                    SatildiMi = false
                },
                new Araba
                {
                    Marka = "Audi",
                    Model = "A4",
                    Yil = 2021,
                    Fiyat = 1100000,
                    Kilometre = 15000,
                    YakıtTipi = "Dizel",
                    VitesTipi = "Otomatik",
                    Renk = "Gri",
                    Aciklama = "Yeni gibi, çok az kullanılmış.",
                    ResimUrl = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800",
                    SatildiMi = false
                },
                new Araba
                {
                    Marka = "Volkswagen",
                    Model = "Golf",
                    Yil = 2018,
                    Fiyat = 450000,
                    Kilometre = 50000,
                    YakıtTipi = "Dizel",
                    VitesTipi = "Manuel",
                    Renk = "Kırmızı",
                    Aciklama = "Ekonomik ve güvenilir.",
                    ResimUrl = "https://images.unsplash.com/photo-1618767747322-64e376fd4826?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    SatildiMi = false
                },
                new Araba
                {
                    Marka = "Ford",
                    Model = "Focus",
                    Yil = 2020,
                    Fiyat = 380000,
                    Kilometre = 40000,
                    YakıtTipi = "Benzin",
                    VitesTipi = "Otomatik",
                    Renk = "Mavi",
                    Aciklama = "Aile için ideal, geniş bagaj.",
                    ResimUrl = "https://images.unsplash.com/photo-1648215115528-a328bb18c980?q=80&w=1976&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    SatildiMi = false
                }
            };

            foreach (Araba araba in arabalar)
            {
                context.Arabalar.Add(araba);
            }

            context.SaveChanges();
        }
    }
}




