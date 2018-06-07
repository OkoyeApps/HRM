namespace resourceEdge.Domain.Migrations.EdgeDbContext2
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeArchiveadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Announcements", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Announcements", "LocationId", "dbo.Locations");
            DropIndex("dbo.Announcements", new[] { "GroupId" });
            DropIndex("dbo.Announcements", new[] { "LocationId" });
            CreateTable(
                "dbo.AdditionalDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Email = c.String(),
                        PermanentStreet = c.String(),
                        PermanentCountry = c.String(),
                        PermanentState = c.String(),
                        PermanentPostalCode = c.String(),
                        TemptStreet = c.String(),
                        TempCountry = c.String(),
                        TempState = c.String(),
                        TempPostalCode = c.String(),
                        EmergencyName = c.String(),
                        EmergencyEmail = c.String(),
                        EmergencyNumber = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.CooperateCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        CardType = c.String(),
                        CardNumber = c.String(),
                        CardName = c.String(),
                        ExpiryDate = c.DateTime(),
                        IssuedBy = c.String(),
                        Code = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Dependencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        DependentName = c.String(),
                        DependentRelationId = c.Int(nullable: false),
                        DOBOfDependent = c.DateTime(),
                        dependentAge = c.Int(nullable: false),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                        DependencyRelation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DependencyRelations", t => t.DependencyRelation_ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId)
                .Index(t => t.DependencyRelation_ID);
            
            CreateTable(
                "dbo.DependencyRelations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Disabilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Name = c.String(),
                        FileUrl = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        EducationlevelId = c.Int(nullable: false),
                        InstitutionName = c.String(),
                        Course = c.String(),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EducationLevels", t => t.EducationlevelId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId)
                .Index(t => t.EducationlevelId);
            
            CreateTable(
                "dbo.EducationLevels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        CompanyName = c.String(),
                        CompanyWebsite = c.String(),
                        Designation = c.String(),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        ReasonForLeaving = c.String(),
                        ReferrerName = c.String(),
                        ReferrerContact = c.String(),
                        ReferrerEmail = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.JobHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        positionId = c.Int(nullable: false),
                        From = c.DateTime(),
                        To = c.DateTime(),
                        AmountRecieved = c.Double(nullable: false),
                        AmountPaid = c.Double(nullable: false),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Jobtitles", t => t.JobId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.Positions", t => t.positionId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId)
                .Index(t => t.DepartmentId)
                .Index(t => t.JobId)
                .Index(t => t.positionId);
            
            CreateTable(
                "dbo.MedicalClaims",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        medicalClaimId = c.Int(nullable: false),
                        DateOfClaim = c.DateTime(),
                        Severity = c.Int(nullable: false),
                        Description = c.String(),
                        MedicalInsurer = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.MedicalClaimTypes", t => t.medicalClaimId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId)
                .Index(t => t.medicalClaimId);
            
            CreateTable(
                "dbo.MedicalClaimTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Personals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        MaritalStatus = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                        BloodGroup = c.String(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Name = c.String(),
                        ExperienceYear = c.Int(nullable: false),
                        LastUsedDate = c.DateTime(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.TrainingAndCertifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        CreatedBY = c.String(),
                        CourseName = c.String(),
                        CourseLevel = c.String(),
                        CouserOfferedBy = c.String(),
                        Description = c.String(),
                        CertificateName = c.String(),
                        IssuedDate = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Visas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        PassportNumber = c.String(),
                        IssuedDate = c.DateTime(),
                        ExpiryDate = c.DateTime(),
                        VisaTypeCode = c.String(),
                        VisaNumber = c.Int(nullable: false),
                        VisaIssuedDate = c.DateTime(),
                        VisaExpiryDate = c.DateTime(),
                        oneToNineStatus = c.String(),
                        oneToNineReviewDate = c.DateTime(nullable: false),
                        IssuingAuthority = c.String(),
                        OneToNightyFourStatus = c.String(),
                        OneToNightyFourExpiryDate = c.DateTime(),
                        CreatedBY = c.String(),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.EmployeeId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId);
            
            DropTable("dbo.Announcements");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        GroupId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Createdon = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Visas", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Visas", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Visas", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TrainingAndCertifications", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.TrainingAndCertifications", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.TrainingAndCertifications", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Skills", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Skills", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Skills", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Personals", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Personals", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Personals", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.MedicalClaims", "medicalClaimId", "dbo.MedicalClaimTypes");
            DropForeignKey("dbo.MedicalClaims", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.MedicalClaims", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.MedicalClaims", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.JobHistories", "positionId", "dbo.Positions");
            DropForeignKey("dbo.JobHistories", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.JobHistories", "JobId", "dbo.Jobtitles");
            DropForeignKey("dbo.JobHistories", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.JobHistories", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.JobHistories", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Experiences", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Experiences", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Experiences", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Educations", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Educations", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Educations", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Educations", "EducationlevelId", "dbo.EducationLevels");
            DropForeignKey("dbo.Documents", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Documents", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Documents", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Disabilities", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Disabilities", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Disabilities", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Dependencies", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Dependencies", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Dependencies", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Dependencies", "DependencyRelation_ID", "dbo.DependencyRelations");
            DropForeignKey("dbo.CooperateCards", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.CooperateCards", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.CooperateCards", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Contacts", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Contacts", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Contacts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AdditionalDetails", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.AdditionalDetails", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.AdditionalDetails", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Visas", new[] { "LocationId" });
            DropIndex("dbo.Visas", new[] { "GroupId" });
            DropIndex("dbo.Visas", new[] { "EmployeeId" });
            DropIndex("dbo.TrainingAndCertifications", new[] { "LocationId" });
            DropIndex("dbo.TrainingAndCertifications", new[] { "GroupId" });
            DropIndex("dbo.TrainingAndCertifications", new[] { "EmployeeId" });
            DropIndex("dbo.Skills", new[] { "LocationId" });
            DropIndex("dbo.Skills", new[] { "GroupId" });
            DropIndex("dbo.Skills", new[] { "EmployeeId" });
            DropIndex("dbo.Personals", new[] { "LocationId" });
            DropIndex("dbo.Personals", new[] { "GroupId" });
            DropIndex("dbo.Personals", new[] { "EmployeeId" });
            DropIndex("dbo.MedicalClaims", new[] { "medicalClaimId" });
            DropIndex("dbo.MedicalClaims", new[] { "LocationId" });
            DropIndex("dbo.MedicalClaims", new[] { "GroupId" });
            DropIndex("dbo.MedicalClaims", new[] { "EmployeeId" });
            DropIndex("dbo.JobHistories", new[] { "positionId" });
            DropIndex("dbo.JobHistories", new[] { "JobId" });
            DropIndex("dbo.JobHistories", new[] { "DepartmentId" });
            DropIndex("dbo.JobHistories", new[] { "LocationId" });
            DropIndex("dbo.JobHistories", new[] { "GroupId" });
            DropIndex("dbo.JobHistories", new[] { "EmployeeId" });
            DropIndex("dbo.Experiences", new[] { "LocationId" });
            DropIndex("dbo.Experiences", new[] { "GroupId" });
            DropIndex("dbo.Experiences", new[] { "EmployeeId" });
            DropIndex("dbo.Educations", new[] { "EducationlevelId" });
            DropIndex("dbo.Educations", new[] { "LocationId" });
            DropIndex("dbo.Educations", new[] { "GroupId" });
            DropIndex("dbo.Educations", new[] { "EmployeeId" });
            DropIndex("dbo.Documents", new[] { "LocationId" });
            DropIndex("dbo.Documents", new[] { "GroupId" });
            DropIndex("dbo.Documents", new[] { "EmployeeId" });
            DropIndex("dbo.Disabilities", new[] { "LocationId" });
            DropIndex("dbo.Disabilities", new[] { "GroupId" });
            DropIndex("dbo.Disabilities", new[] { "EmployeeId" });
            DropIndex("dbo.Dependencies", new[] { "DependencyRelation_ID" });
            DropIndex("dbo.Dependencies", new[] { "LocationId" });
            DropIndex("dbo.Dependencies", new[] { "GroupId" });
            DropIndex("dbo.Dependencies", new[] { "EmployeeId" });
            DropIndex("dbo.CooperateCards", new[] { "LocationId" });
            DropIndex("dbo.CooperateCards", new[] { "GroupId" });
            DropIndex("dbo.CooperateCards", new[] { "EmployeeId" });
            DropIndex("dbo.Contacts", new[] { "LocationId" });
            DropIndex("dbo.Contacts", new[] { "GroupId" });
            DropIndex("dbo.Contacts", new[] { "EmployeeId" });
            DropIndex("dbo.AdditionalDetails", new[] { "LocationId" });
            DropIndex("dbo.AdditionalDetails", new[] { "GroupId" });
            DropIndex("dbo.AdditionalDetails", new[] { "EmployeeId" });
            DropTable("dbo.Visas");
            DropTable("dbo.TrainingAndCertifications");
            DropTable("dbo.Skills");
            DropTable("dbo.Personals");
            DropTable("dbo.MedicalClaimTypes");
            DropTable("dbo.MedicalClaims");
            DropTable("dbo.JobHistories");
            DropTable("dbo.Experiences");
            DropTable("dbo.EducationLevels");
            DropTable("dbo.Educations");
            DropTable("dbo.Documents");
            DropTable("dbo.Disabilities");
            DropTable("dbo.DependencyRelations");
            DropTable("dbo.Dependencies");
            DropTable("dbo.CooperateCards");
            DropTable("dbo.Contacts");
            DropTable("dbo.AdditionalDetails");
            CreateIndex("dbo.Announcements", "LocationId");
            CreateIndex("dbo.Announcements", "GroupId");
            AddForeignKey("dbo.Announcements", "LocationId", "dbo.Locations", "Id");
            AddForeignKey("dbo.Announcements", "GroupId", "dbo.Groups", "Id");
        }
    }
}
