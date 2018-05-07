namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetRequisitionEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestAssets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssetCategoryId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        DueTime = c.DateTime(),
                        RequestedBy = c.String(),
                        createdBy = c.String(),
                        CreatedByFullName = c.String(),
                        ModifiedBy = c.String(),
                        IsResolved = c.Boolean(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetCategories", t => t.AssetCategoryId)
                .Index(t => t.AssetCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestAssets", "AssetCategoryId", "dbo.AssetCategories");
            DropIndex("dbo.RequestAssets", new[] { "AssetCategoryId" });
            DropTable("dbo.RequestAssets");
        }
    }
}
