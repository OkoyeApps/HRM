namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppraisalConfigurationAssociationUpdate : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AppraisalConfigurations", "BusinessUnitId");
            CreateIndex("dbo.AppraisalConfigurations", "DepartmentId");
            AddForeignKey("dbo.AppraisalConfigurations", "BusinessUnitId", "dbo.BusinessUnits", "Id");
            AddForeignKey("dbo.AppraisalConfigurations", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppraisalConfigurations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AppraisalConfigurations", "BusinessUnitId", "dbo.BusinessUnits");
            DropIndex("dbo.AppraisalConfigurations", new[] { "DepartmentId" });
            DropIndex("dbo.AppraisalConfigurations", new[] { "BusinessUnitId" });
        }
    }
}
