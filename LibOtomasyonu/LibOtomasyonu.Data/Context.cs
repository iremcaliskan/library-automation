using LibOtomasyonu.Data.Migrations;
using LibOtomasyonu.Data.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LibOtomasyonu.Data
{
    public class Context: DbContext
    {
        //Connection str ekledim.
        public Context(): base("Context") { //Yapıcı method
            //Code first yaklaşımı ile inşa edilen projede veri tabanında yapılan değişikliklerde, 
            //veri tabanı yeniden oluşturuluyor. İçerisindeki veriler silinmiş oluyor. 
            //Bu durumu engellemek için Migration yapısı kullanılmalı..
            //Migration: Veri tabanı yoksa yenisini oluşturur, varsa değişiklikleri güncelleme işlemi yapar.
            //Nuget Powershell : enable-migrations komutu ile config dosyasında eklemeler yapılır.
            //AutomaticMigrationsEnabled = true;  true yaparak oto güncelleme sağlanır
            //AutomaticMigrationDataLossAllowed = true;  tablo değişikilkleri izin verilir veri olmasına rağmen
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration> ("Context"));
        }

        //Sınıfların set edilmesi
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<OduncKitap> OduncKitaplar { get; set; }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //s takısını kaldırma
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
