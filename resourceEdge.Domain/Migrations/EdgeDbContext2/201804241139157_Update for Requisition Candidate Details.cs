namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateforRequisitionCandidateDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppraisalResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Score = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequisitionId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        ProfileImage = c.String(),
                        Resume = c.String(),
                        Qualification = c.String(),
                        EducationSummary = c.String(),
                        Experience = c.Int(nullable: false),
                        Skills = c.String(),
                        Status = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        City = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId)
                .Index(t => t.RequisitionId);
            
            CreateTable(
                "dbo.CandidateWorkDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        CompanyName = c.String(),
                        CompanyPhoneNumber = c.String(),
                        CompanyAddress = c.String(),
                        CompanyWebsite = c.String(),
                        CompanyDesignation = c.String(),
                        CompanyFrom = c.DateTime(),
                        CompanyTo = c.DateTime(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidates", t => t.CandidateId)
                .Index(t => t.CandidateId);
            
            AlterColumn("dbo.Requisitions", "AppStatus1", c => c.Boolean());
            AlterColumn("dbo.Requisitions", "AppStatus2", c => c.Boolean());
            AlterColumn("dbo.Requisitions", "AppStatus3", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateWorkDetails", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Candidates", "RequisitionId", "dbo.Requisitions");
            DropIndex("dbo.CandidateWorkDetails", new[] { "CandidateId" });
            DropIndex("dbo.Candidates", new[] { "RequisitionId" });
            AlterColumn("dbo.Requisitions", "AppStatus3", c => c.String());
            AlterColumn("dbo.Requisitions", "AppStatus2", c => c.String());
            AlterColumn("dbo.Requisitions", "AppStatus1", c => c.String());
            DropTable("dbo.CandidateWorkDetails");
            DropTable("dbo.Candidates");
            DropTable("dbo.AppraisalResults");
        }
    }
}
