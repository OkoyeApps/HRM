namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentLocationandGroupAssociationAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "LocationId", c => c.Int());
            AddColumn("dbo.Departments", "GroupId", c => c.Int());
            CreateIndex("dbo.Departments", "LocationId");
            CreateIndex("dbo.Departments", "GroupId");
            AddForeignKey("dbo.Departments", "GroupId", "dbo.Groups", "Id");
            AddForeignKey("dbo.Departments", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Departments", "GroupId", "dbo.Groups");
            DropIndex("dbo.Departments", new[] { "GroupId" });
            DropIndex("dbo.Departments", new[] { "LocationId" });
            DropColumn("dbo.Departments", "GroupId");
            DropColumn("dbo.Departments", "LocationId");
        }
    }
}
