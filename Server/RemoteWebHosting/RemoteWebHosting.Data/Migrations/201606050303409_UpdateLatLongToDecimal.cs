namespace RemoteWebHosting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLatLongToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HarResults", "From_Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HarResults", "From_Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HarResults", "To_Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HarResults", "To_Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HarResults", "To_Longitude", c => c.String());
            AlterColumn("dbo.HarResults", "To_Latitude", c => c.String());
            AlterColumn("dbo.HarResults", "From_Longitude", c => c.String());
            AlterColumn("dbo.HarResults", "From_Latitude", c => c.String());
        }
    }
}
