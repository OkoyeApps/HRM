namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetorequisitionAssociation : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Requisitions", "PositionId");
            CreateIndex("dbo.Requisitions", "BusinessUnitId");
            CreateIndex("dbo.Requisitions", "DepartmentId");
            CreateIndex("dbo.Requisitions", "JobTitleId");
            AddForeignKey("dbo.Requisitions", "BusinessUnitId", "dbo.BusinessUnits", "Id");
            AddForeignKey("dbo.Requisitions", "DepartmentId", "dbo.Departments", "Id");
            AddForeignKey("dbo.Requisitions", "JobTitleId", "dbo.Jobtitles", "Id");
            AddForeignKey("dbo.Requisitions", "PositionId", "dbo.Positions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requisitions", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Requisitions", "JobTitleId", "dbo.Jobtitles");
            DropForeignKey("dbo.Requisitions", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Requisitions", "BusinessUnitId", "dbo.BusinessUnits");
            DropIndex("dbo.Requisitions", new[] { "JobTitleId" });
            DropIndex("dbo.Requisitions", new[] { "DepartmentId" });
            DropIndex("dbo.Requisitions", new[] { "BusinessUnitId" });
            DropIndex("dbo.Requisitions", new[] { "PositionId" });
        }
    }
}
