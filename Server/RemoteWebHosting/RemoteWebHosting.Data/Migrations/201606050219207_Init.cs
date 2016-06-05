namespace RemoteWebHosting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HarDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Blog = c.String(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HarResults",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NodeId = c.Guid(nullable: false),
                        NodeName = c.String(),
                        TestTime = c.DateTimeOffset(nullable: false, precision: 7),
                        Ip = c.String(),
                        Dns = c.String(),
                        From_Latitude = c.String(),
                        From_Longitude = c.String(),
                        To_Latitude = c.String(),
                        To_Longitude = c.String(),
                        Ping = c.Int(nullable: false),
                        TotalTime = c.Long(nullable: false),
                        TotalSize = c.Long(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        HarData_Id = c.Guid(),
                        Test_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HarDatas", t => t.HarData_Id)
                .ForeignKey("dbo.WebTests", t => t.Test_Id)
                .Index(t => t.HarData_Id)
                .Index(t => t.Test_Id);
            
            CreateTable(
                "dbo.WebTests",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HarResults", "Test_Id", "dbo.WebTests");
            DropForeignKey("dbo.HarResults", "HarData_Id", "dbo.HarDatas");
            DropIndex("dbo.HarResults", new[] { "Test_Id" });
            DropIndex("dbo.HarResults", new[] { "HarData_Id" });
            DropTable("dbo.WebTests");
            DropTable("dbo.HarResults");
            DropTable("dbo.HarDatas");
        }
    }
}
