namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InterviewdetailUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateInterviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InterviewId = c.Int(nullable: false),
                        CandidateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Candidates", t => t.CandidateId)
                .ForeignKey("dbo.Interviews", t => t.InterviewId)
                .Index(t => t.InterviewId)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "dbo.Interviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Interviewer = c.String(),
                        LocationId = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        City = c.String(),
                        InterviewTypeId = c.Int(nullable: false),
                        InterviewStatusId = c.Int(nullable: false),
                        RequisitionId = c.Int(nullable: false),
                        InterviewDate = c.DateTime(nullable: false),
                        InterviewTime = c.DateTime(nullable: false),
                        InterviewName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InterviewStatus", t => t.InterviewStatusId)
                .ForeignKey("dbo.InterviewTypes", t => t.InterviewTypeId)
                .ForeignKey("dbo.Requisitions", t => t.RequisitionId)
                .Index(t => t.InterviewTypeId)
                .Index(t => t.InterviewStatusId)
                .Index(t => t.RequisitionId);
            
            CreateTable(
                "dbo.InterviewStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InterviewTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CandidateStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interviews", "RequisitionId", "dbo.Requisitions");
            DropForeignKey("dbo.Interviews", "InterviewTypeId", "dbo.InterviewTypes");
            DropForeignKey("dbo.Interviews", "InterviewStatusId", "dbo.InterviewStatus");
            DropForeignKey("dbo.CandidateInterviews", "InterviewId", "dbo.Interviews");
            DropForeignKey("dbo.CandidateInterviews", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Interviews", new[] { "RequisitionId" });
            DropIndex("dbo.Interviews", new[] { "InterviewStatusId" });
            DropIndex("dbo.Interviews", new[] { "InterviewTypeId" });
            DropIndex("dbo.CandidateInterviews", new[] { "CandidateId" });
            DropIndex("dbo.CandidateInterviews", new[] { "InterviewId" });
            DropTable("dbo.CandidateStatus");
            DropTable("dbo.InterviewTypes");
            DropTable("dbo.InterviewStatus");
            DropTable("dbo.Interviews");
            DropTable("dbo.CandidateInterviews");
        }
    }
}
