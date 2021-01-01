namespace LibOtomasyonu.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUye : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uye", "Yetki", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uye", "Yetki");
        }
    }
}
