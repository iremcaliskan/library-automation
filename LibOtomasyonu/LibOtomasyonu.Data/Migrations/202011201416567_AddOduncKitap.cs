namespace LibOtomasyonu.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOduncKitap : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.OduncKitap", "KitapId");
            AddForeignKey("dbo.OduncKitap", "KitapId", "dbo.Kitap", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OduncKitap", "KitapId", "dbo.Kitap");
            DropIndex("dbo.OduncKitap", new[] { "KitapId" });
        }
    }
}
