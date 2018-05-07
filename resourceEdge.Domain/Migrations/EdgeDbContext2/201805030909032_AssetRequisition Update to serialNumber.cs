namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetRequisitionUpdatetoserialNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "SerialNumber", c => c.Long(nullable: false));
            DropColumn("dbo.Assets", "SeriaNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Assets", "SeriaNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Assets", "SerialNumber");
        }
    }
}
