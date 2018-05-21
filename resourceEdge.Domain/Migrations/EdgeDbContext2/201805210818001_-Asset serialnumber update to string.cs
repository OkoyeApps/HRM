namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Assetserialnumberupdatetostring : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Assets", "SerialNumber");
            AddColumn("dbo.Assets", "SerialNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assets", "SerialNumber", c => c.Long(nullable: false));
        }
    }
}
