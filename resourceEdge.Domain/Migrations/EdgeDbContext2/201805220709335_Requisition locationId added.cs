namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionlocationIdadded : DbMigration
    {
        public override void Up()
        {
            //RenameColumn("dbo.Requisition", "groupId", "GroupId");
            DropColumn("dbo.Requisitions", "groupId");
            AddColumn("dbo.Requisitions", "LocationId", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.Requisitions", "GroupId", c => c.Int(nullable: false, defaultValue: 1));
          
            CreateIndex("dbo.Requisitions", "GroupId");
            CreateIndex("dbo.Requisitions", "LocationId");
            AddForeignKey("dbo.Requisitions", "GroupId", "dbo.Groups", "Id");
            AddForeignKey("dbo.Requisitions", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requisitions", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Requisitions", "GroupId", "dbo.Groups");
            DropIndex("dbo.Requisitions", new[] { "LocationId" });
            DropIndex("dbo.Requisitions", new[] { "GroupId" });
            DropColumn("dbo.Requisitions", "LocationId");
        }
    }
}
