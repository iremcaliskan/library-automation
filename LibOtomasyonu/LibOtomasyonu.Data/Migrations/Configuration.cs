namespace LibOtomasyonu.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LibOtomasyonu.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; //true yaparak oto güncelleme sağlanır
            AutomaticMigrationDataLossAllowed = true; //tablo değişikilkleri izin verilir veri olmasına rağmen
            ContextKey = "LibOtomasyonu.Data.Context";
        }

        protected override void Seed(LibOtomasyonu.Data.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
