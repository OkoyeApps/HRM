namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetoDisciplinemodel4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DisciplinaryIncidents", "BusinessUnitId", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("dbo.DisciplinaryIncidents", "DepartmentId", c => c.Int(nullable: false, defaultValue: 2));
            CreateIndex("dbo.DisciplinaryIncidents", "BusinessUnitId");
            CreateIndex("dbo.DisciplinaryIncidents", "DepartmentId");
            AddForeignKey("dbo.DisciplinaryIncidents", "BusinessUnitId", "dbo.BusinessUnits", "Id");
            AddForeignKey("dbo.DisciplinaryIncidents", "DepartmentId", "dbo.Departments", "Id");
            DropColumn("dbo.DisciplinaryIncidents", "BusinessUnit");
            DropColumn("dbo.DisciplinaryIncidents", "Department");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DisciplinaryIncidents", "Department", c => c.Int(nullable: false));
            AddColumn("dbo.DisciplinaryIncidents", "BusinessUnit", c => c.Int(nullable: false));
            DropForeignKey("dbo.DisciplinaryIncidents", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DisciplinaryIncidents", "BusinessUnitId", "dbo.BusinessUnits");
            DropIndex("dbo.DisciplinaryIncidents", new[] { "DepartmentId" });
            DropIndex("dbo.DisciplinaryIncidents", new[] { "BusinessUnitId" });
            DropColumn("dbo.DisciplinaryIncidents", "DepartmentId");
            DropColumn("dbo.DisciplinaryIncidents", "BusinessUnitId");
        }
    }
}
