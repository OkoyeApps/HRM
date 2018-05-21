namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetoAssetGroupandLocation1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Assets", "GroupId");
            AddColumn("dbo.Assets", "LocationId", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("dbo.Assets", "GroupId", c => c.Int(nullable: false, defaultValue: 1));
            //RenameColumn("dbo.Assets", "groupId", "GroupId", new { defaultValue = 1 });
            CreateIndex("dbo.Assets", "GroupId");
            CreateIndex("dbo.Assets", "LocationId");
            AddForeignKey("dbo.Assets", "GroupId", "dbo.Groups", "Id");
            AddForeignKey("dbo.Assets", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Assets", "GroupId", "dbo.Groups");
            DropIndex("dbo.Assets", new[] { "LocationId" });
            DropIndex("dbo.Assets", new[] { "GroupId" });
            DropColumn("dbo.Assets", "LocationId");
            DropColumn("dbo.Assets", "GroupId");
        }
    }
}
