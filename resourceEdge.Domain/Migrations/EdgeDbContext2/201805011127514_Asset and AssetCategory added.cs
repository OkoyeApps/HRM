namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetandAssetCategoryadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SeriaNumber = c.Int(nullable: false),
                        IsInUse = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                        AssetCategoryId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AssetCategories", t => t.AssetCategoryId)
                .Index(t => t.AssetCategoryId);
            
            CreateTable(
                "dbo.AssetCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "AssetCategoryId", "dbo.AssetCategories");
            DropIndex("dbo.Assets", new[] { "AssetCategoryId" });
            DropTable("dbo.AssetCategories");
            DropTable("dbo.Assets");
        }
    }
}
