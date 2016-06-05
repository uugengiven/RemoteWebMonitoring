namespace RemoteWebHosting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingPrimaryKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HarResults", "HarData_Id", "dbo.HarDatas");
            DropPrimaryKey("dbo.HarDatas");
            DropPrimaryKey("dbo.HarResults");
            AlterColumn("dbo.HarDatas", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newid()"));
            AlterColumn("dbo.HarResults", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newid()"));
            AddPrimaryKey("dbo.HarDatas", "Id");
            AddPrimaryKey("dbo.HarResults", "Id");
            AddForeignKey("dbo.HarResults", "HarData_Id", "dbo.HarDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HarResults", "HarData_Id", "dbo.HarDatas");
            DropPrimaryKey("dbo.HarResults");
            DropPrimaryKey("dbo.HarDatas");
            AlterColumn("dbo.HarResults", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.HarDatas", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.HarResults", "Id");
            AddPrimaryKey("dbo.HarDatas", "Id");
            AddForeignKey("dbo.HarResults", "HarData_Id", "dbo.HarDatas", "Id");
        }
    }
}
