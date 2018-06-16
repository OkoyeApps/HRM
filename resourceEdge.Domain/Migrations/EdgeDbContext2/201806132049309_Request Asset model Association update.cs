namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestAssetmodelAssociationupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestAssets", "AssetId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.RequestAssets", "AssetId");
            AddForeignKey("dbo.RequestAssets", "AssetId", "dbo.Assets", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestAssets", "AssetId", "dbo.Assets");
            DropIndex("dbo.RequestAssets", new[] { "AssetId" });
            DropColumn("dbo.RequestAssets", "AssetId");
        }
    }
}
