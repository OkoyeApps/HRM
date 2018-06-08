namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reportmanagermodelassociationupdated : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ReportManagers", "BusinessUnitId");
            CreateIndex("dbo.ReportManagers", "employeeId");
            CreateIndex("dbo.ReportManagers", "GroupId");
            CreateIndex("dbo.ReportManagers", "LocationId");
            AddForeignKey("dbo.ReportManagers", "BusinessUnitId", "dbo.BusinessUnits", "Id");
            AddForeignKey("dbo.ReportManagers", "employeeId", "dbo.Employees", "ID");
            AddForeignKey("dbo.ReportManagers", "GroupId", "dbo.Groups", "Id");
            AddForeignKey("dbo.ReportManagers", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportManagers", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.ReportManagers", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.ReportManagers", "employeeId", "dbo.Employees");
            DropForeignKey("dbo.ReportManagers", "BusinessUnitId", "dbo.BusinessUnits");
            DropIndex("dbo.ReportManagers", new[] { "LocationId" });
            DropIndex("dbo.ReportManagers", new[] { "GroupId" });
            DropIndex("dbo.ReportManagers", new[] { "employeeId" });
            DropIndex("dbo.ReportManagers", new[] { "BusinessUnitId" });
        }
    }
}
