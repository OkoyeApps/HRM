namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllDisciplineandrelatedmodeladded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consequences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DisciplinaryIncidents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RaisedBy = c.Int(nullable: false),
                        BusinessUnit = c.Int(nullable: false),
                        Department = c.Int(nullable: false),
                        EmployeeName = c.String(),
                        JobTitle = c.Int(),
                        ReportingManager = c.String(),
                        DateOfOccurence = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        ViolationId = c.Int(nullable: false),
                        ViolationDescription = c.String(),
                        Verdict = c.Int(nullable: false),
                        CorrectiveAction = c.Int(nullable: false),
                        EmployeeAppeal = c.Int(nullable: false),
                        EmployeeStatement = c.String(),
                        ConsequenceId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consequences", t => t.ConsequenceId)
                .ForeignKey("dbo.Violations", t => t.ViolationId)
                .Index(t => t.ViolationId)
                .Index(t => t.ConsequenceId);
            
            CreateTable(
                "dbo.Violations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DisciplinaryIncidents", "ViolationId", "dbo.Violations");
            DropForeignKey("dbo.DisciplinaryIncidents", "ConsequenceId", "dbo.Consequences");
            DropIndex("dbo.DisciplinaryIncidents", new[] { "ConsequenceId" });
            DropIndex("dbo.DisciplinaryIncidents", new[] { "ViolationId" });
            DropTable("dbo.Violations");
            DropTable("dbo.DisciplinaryIncidents");
            DropTable("dbo.Consequences");
        }
    }
}
