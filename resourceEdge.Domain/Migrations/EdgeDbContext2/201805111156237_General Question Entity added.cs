namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeneralQuestionEntityadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeneralQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(),
                        BusinessUnitId = c.Int(),
                        DepartmentId = c.Int(),
                        Question = c.String(),
                        Description = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessUnits", t => t.BusinessUnitId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId)
                .Index(t => t.BusinessUnitId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeneralQuestions", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.GeneralQuestions", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GeneralQuestions", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.GeneralQuestions", "BusinessUnitId", "dbo.BusinessUnits");
            DropIndex("dbo.GeneralQuestions", new[] { "DepartmentId" });
            DropIndex("dbo.GeneralQuestions", new[] { "BusinessUnitId" });
            DropIndex("dbo.GeneralQuestions", new[] { "LocationId" });
            DropIndex("dbo.GeneralQuestions", new[] { "GroupId" });
            DropTable("dbo.GeneralQuestions");
        }
    }
}
