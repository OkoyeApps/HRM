namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetAssetCategoryandRequestAssetgroupandlocationUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "groupId", c => c.Int(nullable: false));
            AddColumn("dbo.AssetCategories", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.RequestAssets", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.RequestAssets", "LocationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestAssets", "LocationId");
            DropColumn("dbo.RequestAssets", "GroupId");
            DropColumn("dbo.AssetCategories", "GroupId");
            DropColumn("dbo.Assets", "groupId");
        }
    }
}
