namespace resourceEdge.Domain.Migrations.EdgeDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatetotheAllmodelAssociations : DbMigration
    {
        public override void Up()
        {
            Sql(@"ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_dbo.Employees_dbo.Departments_Departments_DeptId]");
            DropForeignKey("dbo.ReportManagers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Positions", "jobtitleid", "dbo.Jobtitles");
            DropIndex("dbo.Employees", new[] { "Departments_DeptId" });
            DropIndex("dbo.Positions", new[] { "jobtitleid" });
            DropColumn("dbo.Employees", "departmentId");

            DropPrimaryKey("dbo.Departments");
            DropPrimaryKey("dbo.IdentityCodes");
            DropPrimaryKey("dbo.Jobtitles");
            DropPrimaryKey("dbo.Positions");
            DropPrimaryKey("dbo.Prefixes");

            DropColumn("dbo.Departments", "DeptId");
            DropColumn("dbo.IdentityCodes", "codeId");
            DropColumn("dbo.Jobtitles", "JobId");
            DropColumn("dbo.Positions", "PosId");
            DropColumn("dbo.Prefixes", "prefixId");
            DropColumn("dbo.Employees", "Departments_DeptId");
            //DropColumn("dbo.Employees", "departmentId");
            //DropForeignKey("dbo.Employees", "Departments_DeptId", "dbo.Departments");
            //RenameColumn(table: "dbo.Departments", name: "BunitId", newName: "BusinessUnitsId");
            //RenameColumn(table: "dbo.Employees", name: "departmentId", newName: "DepartmentId");
            //RenameIndex(table: "dbo.Departments", name: "IX_BunitId", newName: "IX_BusinessUnitsId");
            AddColumn("dbo.Departments", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.IdentityCodes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Jobtitles", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Positions", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Prefixes", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false, defaultValue: 0));
            AddPrimaryKey("dbo.Departments", "ID");
            AddPrimaryKey("dbo.IdentityCodes", "Id");
            AddPrimaryKey("dbo.Jobtitles", "Id");
            AddPrimaryKey("dbo.Positions", "Id");
            AddPrimaryKey("dbo.Prefixes", "Id");
            //AlterColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "DepartmentId");
            CreateIndex("dbo.Jobtitles", "GroupId");
            CreateIndex("dbo.Positions", "JobtitleId");
            AddForeignKey("dbo.Jobtitles", "GroupId", "dbo.Groups", "Id");
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "ID");
            AddForeignKey("dbo.ReportManagers", "DepartmentId", "dbo.Departments", "ID");
            AddForeignKey("dbo.Positions", "JobtitleId", "dbo.Jobtitles", "Id");

        }
        
        public override void Down()
        {
            AddColumn("dbo.Prefixes", "prefixId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Positions", "PosId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Jobtitles", "JobId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.IdentityCodes", "codeId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Departments", "DeptId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Positions", "JobtitleId", "dbo.Jobtitles");
            DropForeignKey("dbo.ReportManagers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Jobtitles", "GroupId", "dbo.Groups");
            DropIndex("dbo.Positions", new[] { "JobtitleId" });
            DropIndex("dbo.Jobtitles", new[] { "GroupId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropPrimaryKey("dbo.Prefixes");
            DropPrimaryKey("dbo.Positions");
            DropPrimaryKey("dbo.Jobtitles");
            DropPrimaryKey("dbo.IdentityCodes");
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.Employees", "DepartmentId", c => c.Int());
            DropColumn("dbo.Prefixes", "Id");
            DropColumn("dbo.Positions", "Id");
            DropColumn("dbo.Jobtitles", "Id");
            DropColumn("dbo.IdentityCodes", "Id");
            DropColumn("dbo.Departments", "ID");
            AddPrimaryKey("dbo.Prefixes", "prefixId");
            AddPrimaryKey("dbo.Positions", "PosId");
            AddPrimaryKey("dbo.Jobtitles", "JobId");
            AddPrimaryKey("dbo.IdentityCodes", "codeId");
            AddPrimaryKey("dbo.Departments", "DeptId");
            RenameIndex(table: "dbo.Departments", name: "IX_BusinessUnitsId", newName: "IX_BunitId");
            RenameColumn(table: "dbo.Employees", name: "DepartmentId", newName: "Departments_DeptId");
            RenameColumn(table: "dbo.Departments", name: "BusinessUnitsId", newName: "BunitId");
            AddColumn("dbo.Employees", "departmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Positions", "jobtitleid");
            CreateIndex("dbo.Employees", "Departments_DeptId");
            AddForeignKey("dbo.Positions", "jobtitleid", "dbo.Jobtitles", "JobId");
            AddForeignKey("dbo.ReportManagers", "DepartmentId", "dbo.Departments", "DeptId");
            AddForeignKey("dbo.Employees", "Departments_DeptId", "dbo.Departments", "DeptId");
        }
    }
}
