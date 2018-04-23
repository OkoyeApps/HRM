namespace resourceEdge.Domain.Migrations.EdgeDbContext
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    controllername = c.String(),
                    actionname = c.String(),
                    myip = c.String(),
                    parameters = c.String(),
                    requesturl = c.String(),
                    UserId = c.String(),
                    UserName = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AppraisalConfigurations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AppraisalInitializationId = c.Int(nullable: false),
                    LocationId = c.Int(),
                    BusinessUnitId = c.Int(),
                    DepartmentId = c.Int(),
                    AppraisalStatus = c.Int(nullable: false),
                    EnableTo = c.Int(nullable: false),
                    Eligibility = c.String(),
                    Parameters = c.String(),
                    Code = c.String(),
                    LineManager1 = c.String(),
                    LineManager2 = c.String(),
                    LineManager3 = c.String(),
                    Enabled = c.Boolean(),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppraisalInitializations", t => t.AppraisalInitializationId)
                .Index(t => t.AppraisalInitializationId);

            CreateTable(
                "dbo.AppraisalInitializations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GroupId = c.Int(nullable: false),
                    FromYear = c.Int(nullable: false),
                    ToYear = c.Int(nullable: false),
                    AppraisalMode = c.Int(nullable: false),
                    Period = c.Int(nullable: false),
                    RatingType = c.String(),
                    AppraisalStatus = c.Int(nullable: false),
                    InitilizationCode = c.String(),
                    StartDate = c.DateTime(nullable: false),
                    EndDate = c.DateTime(nullable: false),
                    Enable = c.Boolean(),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    IsActive = c.Boolean(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.Groups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GroupName = c.String(),
                    Descriptions = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                    ModifiedDate = c.DateTime(),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AppraisalModes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AppraisalPeriods",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AppraisalQuestions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    QuestionId = c.Int(nullable: false),
                    Answer = c.Int(nullable: false),
                    LineManager1 = c.String(),
                    LinrManager2 = c.String(),
                    LineManager3 = c.String(),
                    LocationId = c.Int(nullable: false),
                    GroupId = c.Int(nullable: false),
                    L1Status = c.Boolean(),
                    L2Status = c.Boolean(),
                    L3Status = c.Boolean(),
                    Configuration_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppraisalConfigurations", t => t.Configuration_Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId)
                .Index(t => t.Configuration_Id);

            CreateTable(
                "dbo.Questions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserIdForQuestion = c.String(),
                    UserFullName = c.String(),
                    EmpQuestion = c.String(),
                    Description = c.String(),
                    GroupId = c.Int(nullable: false),
                    LocationId = c.Int(nullable: false),
                    BusinessUnitId = c.Int(nullable: false),
                    DepartmentId = c.Int(nullable: false),
                    Isactive = c.Boolean(),
                    Approved = c.Boolean(),
                    Createdby = c.String(),
                    ModifiedBy = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isused = c.Boolean(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessUnits", t => t.BusinessUnitId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.GroupId)
                .Index(t => t.LocationId)
                .Index(t => t.BusinessUnitId);

            CreateTable(
                "dbo.BusinessUnits",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GroupId = c.Int(nullable: false),
                    unitname = c.String(),
                    unitcode = c.String(),
                    descriptions = c.String(),
                    startdate = c.DateTime(),
                    reportManager1 = c.String(),
                    reportManager2 = c.String(),
                    LocationId = c.Int(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .Index(t => t.LocationId);

            CreateTable(
                "dbo.Employees",
                c => new
                {
                    empID = c.Int(nullable: false, identity: true),
                    userId = c.String(),
                    empEmail = c.String(),
                    empRoleId = c.Int(nullable: false),
                    GroupId = c.Int(nullable: false),
                    FullName = c.String(),
                    PhoneNumber = c.String(),
                    dateOfJoining = c.DateTime(),
                    dateOfLeaving = c.DateTime(),
                    reportingManager1 = c.String(),
                    reportingManager2 = c.String(),
                    empStatusId = c.String(),
                    businessunitId = c.Int(nullable: false),
                    departmentId = c.Int(nullable: false),
                    jobtitleId = c.Int(nullable: false),
                    positionId = c.Int(nullable: false),
                    yearsExp = c.String(),
                    LevelId = c.Int(nullable: false),
                    LocationId = c.Int(),
                    prefixId = c.Int(nullable: false),
                    officeNumber = c.String(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                    isOrghead = c.Boolean(),
                    modeofEmployement = c.Int(nullable: false),
                    IsUnithead = c.Boolean(),
                    IsDepthead = c.Boolean(),
                    Departments_DeptId = c.Int(),
                })
                .PrimaryKey(t => t.empID)
                .ForeignKey("dbo.Departments", t => t.Departments_DeptId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.Levels", t => t.LevelId)
                .ForeignKey("dbo.Locations", t => t.LocationId)
                .ForeignKey("dbo.BusinessUnits", t => t.businessunitId)
                .Index(t => t.GroupId)
                .Index(t => t.businessunitId)
                .Index(t => t.LevelId)
                .Index(t => t.LocationId)
                .Index(t => t.Departments_DeptId);

            CreateTable(
                "dbo.Departments",
                c => new
                {
                    DeptId = c.Int(nullable: false, identity: true),
                    deptname = c.String(),
                    deptcode = c.String(),
                    descriptions = c.String(),
                    startdate = c.DateTime(),
                    reportManager1 = c.String(),
                    reportManager2 = c.String(),
                    depthead = c.String(),
                    BunitId = c.Int(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.DeptId)
                .ForeignKey("dbo.BusinessUnits", t => t.BunitId)
                .Index(t => t.BunitId);

            CreateTable(
                "dbo.Levels",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GroupId = c.Int(nullable: false),
                    levelNo = c.Int(nullable: false),
                    LevelName = c.String(),
                    EligibleYears = c.Int(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Locations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GroupId = c.Int(nullable: false),
                    State = c.String(),
                    City = c.String(),
                    Country = c.String(),
                    Address1 = c.String(),
                    Address2 = c.String(),
                    LocationHead1 = c.String(),
                    LocationHead2 = c.String(),
                    LocationHead3 = c.String(),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedOn = c.DateTime(),
                    ModifiedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.AppraisalStatus",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AppraisalRatings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Careers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CareerName = c.String(),
                    ShortCode = c.String(),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.CareerPaths",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CarreerId = c.Int(nullable: false),
                    LevelId = c.Int(nullable: false),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(nullable: false),
                    CarrierName_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Careers", t => t.CarrierName_Id)
                .ForeignKey("dbo.Levels", t => t.LevelId)
                .Index(t => t.LevelId)
                .Index(t => t.CarrierName_Id);

            CreateTable(
                "dbo.EmpHolidays",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    user_id = c.Int(),
                    holiday_group_id = c.Int(),
                    createdby = c.Int(),
                    modifiedby = c.Int(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.EmployeeLeaves",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    EmpLeaveLimit = c.Double(),
                    UsedLeaves = c.Double(),
                    AllotedYear = c.Int(nullable: false),
                    Createdby = c.String(),
                    Modifiedby = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isactive = c.Boolean(),
                    IsLeaveTrasnferSet = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.EmploymentStatus",
                c => new
                {
                    empstId = c.Int(nullable: false, identity: true),
                    employemntStatus = c.String(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.empstId);

            CreateTable(
                "dbo.Files",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    FileName = c.String(),
                    FileType = c.Int(nullable: false),
                    FilePath = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.IdentityCodes",
                c => new
                {
                    codeId = c.Int(nullable: false, identity: true),
                    employee_code = c.String(),
                    backgroundagency_code = c.String(),
                    vendors_code = c.String(),
                    staffing_code = c.String(),
                    users_code = c.String(),
                    requisition_code = c.String(),
                    GroupId = c.Int(nullable: false),
                    createdBy = c.String(),
                    createddate = c.DateTime(),
                    modifiedBy = c.String(),
                    modifieddate = c.DateTime(),
                })
                .PrimaryKey(t => t.codeId)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);

            CreateTable(
                "dbo.Jobtitles",
                c => new
                {
                    JobId = c.Int(nullable: false, identity: true),
                    GroupId = c.Int(nullable: false),
                    jobtitlecode = c.String(),
                    jobtitlename = c.String(),
                    jobdescription = c.String(),
                    minexperiencerequired = c.Double(),
                    jobpaygradecode = c.String(),
                    jobpayfrequency = c.String(),
                    comments = c.String(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.JobId);

            CreateTable(
                "dbo.LeaveManagements",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    CalStartMonth = c.String(),
                    WeekendStartDay = c.String(),
                    WeekendEndDay = c.String(),
                    businessunitId = c.Int(),
                    departmentId = c.String(),
                    HrId = c.String(maxLength: 128),
                    HoursDay = c.String(),
                    IsSatHoliday = c.String(),
                    IsHalfday = c.Int(nullable: false),
                    IsLeaveTransfer = c.Int(nullable: false),
                    IsSkipHolidays = c.Int(nullable: false),
                    Descriptions = c.String(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BusinessUnits", t => t.businessunitId)
                .ForeignKey("dbo.ReportManagers", t => t.HrId)
                .Index(t => t.businessunitId)
                .Index(t => t.HrId);

            CreateTable(
                "dbo.ReportManagers",
                c => new
                {
                    ManagerUserId = c.String(nullable: false, maxLength: 128),
                    DepartmentId = c.Int(nullable: false),
                    BusinessUnitId = c.Int(nullable: false),
                    employeeId = c.Int(nullable: false),
                    FullName = c.String(),
                })
                .PrimaryKey(t => t.ManagerUserId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);

            CreateTable(
                "dbo.LeaveManagementSummaries",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    leavemgmt_id = c.Int(),
                    cal_startmonth = c.Int(),
                    cal_startmonthname = c.String(),
                    weekend_startday = c.Int(),
                    weekend_startdayname = c.String(),
                    weekend_endday = c.Int(),
                    weekend_enddayname = c.String(),
                    businessunit_id = c.Int(),
                    businessunit_name = c.String(),
                    department_id = c.Int(),
                    department_name = c.String(),
                    hours_day = c.Int(),
                    is_satholiday = c.String(),
                    is_halfday = c.String(),
                    is_leavetransfer = c.String(),
                    is_skipholidays = c.String(),
                    descriptions = c.String(),
                    createdby = c.Int(),
                    modifiedby = c.Int(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.LeaveRequests",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    Reason = c.String(),
                    ApprovalComment = c.String(),
                    LeavetypeId = c.Int(),
                    LeaveName = c.String(),
                    LeaveDay = c.Boolean(),
                    FromDate = c.DateTime(),
                    ToDate = c.DateTime(),
                    LeaveStatus = c.Boolean(),
                    RepmangId = c.String(),
                    HrId = c.String(),
                    NoOfDays = c.Double(),
                    AppliedleavesCount = c.Double(),
                    Availableleave = c.Int(nullable: false),
                    requestDaysNo = c.Int(nullable: false),
                    Approval1 = c.Boolean(),
                    Approval2 = c.Boolean(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.EmployeeLeaveTypes",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    leavetype = c.String(),
                    numberofdays = c.Int(),
                    leavecode = c.String(),
                    description = c.String(),
                    leavepreallocated = c.String(),
                    leavepredeductable = c.Int(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.Logins",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    SessionID = c.String(),
                    LoginTime = c.DateTime(nullable: false),
                    LogOutTime = c.DateTime(),
                    IsLogIn = c.Boolean(nullable: false),
                    IsLogOut = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.MailDispatchers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FullName = c.String(),
                    Subject = c.String(),
                    Reciever = c.String(),
                    Sender = c.String(),
                    Message = c.String(),
                    TimeToSend = c.DateTime(),
                    Type = c.Int(nullable: false),
                    GroupName = c.String(),
                    Delivered = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Menus",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Name = c.String(),
                    Role = c.String(),
                    Active = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Months",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    MonthId = c.String(),
                    MonthName = c.String(),
                    Createdby = c.String(),
                    Modifiedby = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.MonthLists",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    MonthId = c.String(),
                    MonthCode = c.String(),
                    Description = c.String(),
                    Createdby = c.String(),
                    Modifiedby = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.Parameters",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ParameterName = c.String(),
                    Descriptions = c.String(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.EmpPayrolls",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BusinessUnit = c.String(),
                    CreatedBy = c.String(),
                    CreatedDate = c.DateTime(nullable: false),
                    Deduction = c.Double(),
                    Department = c.String(),
                    EmpName = c.String(),
                    EmpStatus = c.String(),
                    LeaveAllowance = c.Double(),
                    Loan = c.Double(),
                    ModifiedBy = c.String(),
                    ModifiedDate = c.DateTime(nullable: false),
                    Reimbursable = c.Double(),
                    Remarks = c.String(),
                    ResignationDate = c.DateTime(nullable: false),
                    ResumptionDate = c.DateTime(nullable: false),
                    Salary = c.Double(),
                    Total = c.Double(),
                    UserId = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Positions",
                c => new
                {
                    PosId = c.Int(nullable: false, identity: true),
                    positionname = c.String(),
                    jobtitleid = c.Int(nullable: false),
                    description = c.String(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.PosId)
                .ForeignKey("dbo.Jobtitles", t => t.jobtitleid)
                .Index(t => t.jobtitleid);

            CreateTable(
                "dbo.Prefixes",
                c => new
                {
                    prefixId = c.Int(nullable: false, identity: true),
                    prefixName = c.String(),
                    description = c.String(),
                    createdby = c.String(),
                    modifiedby = c.String(),
                    createddate = c.DateTime(),
                    modifieddate = c.DateTime(),
                    isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.prefixId);

            CreateTable(
                "dbo.products",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    descriptions = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Ratings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    RatingValue = c.Int(),
                    RatingText = c.String(),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Requisitions",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    RequisitionCode = c.String(),
                    OnboardDate = c.DateTime(),
                    PositionId = c.Int(),
                    ReportingId = c.String(),
                    BusinessunitId = c.Int(),
                    DepartmentId = c.Int(),
                    JobTitle = c.Int(),
                    ReqNoPositions = c.String(),
                    SelectedMembers = c.String(),
                    FilledPositions = c.String(),
                    JobDescription = c.String(),
                    ReqSkills = c.String(),
                    ReqQualification = c.String(),
                    ReqExpYears = c.String(),
                    EmpType = c.String(),
                    ReqPriority = c.String(),
                    AdditionalInfo = c.String(),
                    ReqStatus = c.String(),
                    Approver1 = c.String(),
                    Approver2 = c.String(),
                    Approver3 = c.String(),
                    AppStatus1 = c.String(),
                    AppStatus2 = c.String(),
                    AppStatus3 = c.String(),
                    Recruiters = c.String(),
                    ClientId = c.String(),
                    Createdby = c.String(),
                    Modifiedby = c.String(),
                    CreatedDate = c.DateTime(),
                    modifiedDate = c.DateTime(),
                    Isactive = c.String(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.SubscribedAppraisals",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    Code = c.String(),
                    GroupId = c.Int(nullable: false),
                    LocationId = c.Int(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    AppraisalInitializationId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AppraisalInitializations", t => t.AppraisalInitializationId)
                .Index(t => t.AppraisalInitializationId);

            CreateTable(
                "dbo.Weeks",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    WeekId = c.String(),
                    WeekName = c.String(),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.WeekDays",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    DayName = c.String(),
                    DayShortCode = c.String(),
                    DayLongCode = c.String(),
                    description = c.String(),
                    CreatedBy = c.String(),
                    ModifiedBy = c.String(),
                    CreatedDate = c.DateTime(),
                    ModifiedDate = c.DateTime(),
                    Isactive = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.SubscribedAppraisals", "AppraisalInitializationId", "dbo.AppraisalInitializations");
            DropForeignKey("dbo.Positions", "jobtitleid", "dbo.Jobtitles");
            DropForeignKey("dbo.LeaveManagements", "HrId", "dbo.ReportManagers");
            DropForeignKey("dbo.ReportManagers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.LeaveManagements", "businessunitId", "dbo.BusinessUnits");
            DropForeignKey("dbo.IdentityCodes", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.CareerPaths", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.CareerPaths", "CarrierName_Id", "dbo.Careers");
            DropForeignKey("dbo.AppraisalQuestions", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Questions", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Questions", "BusinessUnitId", "dbo.BusinessUnits");
            DropForeignKey("dbo.BusinessUnits", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Employees", "businessunitId", "dbo.BusinessUnits");
            DropForeignKey("dbo.Employees", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Employees", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.Employees", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Employees", "Departments_DeptId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "BunitId", "dbo.BusinessUnits");
            DropForeignKey("dbo.AppraisalQuestions", "Configuration_Id", "dbo.AppraisalConfigurations");
            DropForeignKey("dbo.AppraisalConfigurations", "AppraisalInitializationId", "dbo.AppraisalInitializations");
            DropForeignKey("dbo.AppraisalInitializations", "GroupId", "dbo.Groups");
            DropIndex("dbo.SubscribedAppraisals", new[] { "AppraisalInitializationId" });
            DropIndex("dbo.Positions", new[] { "jobtitleid" });
            DropIndex("dbo.ReportManagers", new[] { "DepartmentId" });
            DropIndex("dbo.LeaveManagements", new[] { "HrId" });
            DropIndex("dbo.LeaveManagements", new[] { "businessunitId" });
            DropIndex("dbo.IdentityCodes", new[] { "GroupId" });
            DropIndex("dbo.CareerPaths", new[] { "CarrierName_Id" });
            DropIndex("dbo.CareerPaths", new[] { "LevelId" });
            DropIndex("dbo.Locations", new[] { "GroupId" });
            DropIndex("dbo.Departments", new[] { "BunitId" });
            DropIndex("dbo.Employees", new[] { "Departments_DeptId" });
            DropIndex("dbo.Employees", new[] { "LocationId" });
            DropIndex("dbo.Employees", new[] { "LevelId" });
            DropIndex("dbo.Employees", new[] { "businessunitId" });
            DropIndex("dbo.Employees", new[] { "GroupId" });
            DropIndex("dbo.BusinessUnits", new[] { "LocationId" });
            DropIndex("dbo.Questions", new[] { "BusinessUnitId" });
            DropIndex("dbo.Questions", new[] { "LocationId" });
            DropIndex("dbo.Questions", new[] { "GroupId" });
            DropIndex("dbo.AppraisalQuestions", new[] { "Configuration_Id" });
            DropIndex("dbo.AppraisalQuestions", new[] { "QuestionId" });
            DropIndex("dbo.AppraisalInitializations", new[] { "GroupId" });
            DropIndex("dbo.AppraisalConfigurations", new[] { "AppraisalInitializationId" });
            DropTable("dbo.WeekDays");
            DropTable("dbo.Weeks");
            DropTable("dbo.SubscribedAppraisals");
            DropTable("dbo.Requisitions");
            DropTable("dbo.Ratings");
            DropTable("dbo.products");
            DropTable("dbo.Prefixes");
            DropTable("dbo.Positions");
            DropTable("dbo.EmpPayrolls");
            DropTable("dbo.Parameters");
            DropTable("dbo.MonthLists");
            DropTable("dbo.Months");
            DropTable("dbo.Menus");
            DropTable("dbo.MailDispatchers");
            DropTable("dbo.Logins");
            DropTable("dbo.EmployeeLeaveTypes");
            DropTable("dbo.LeaveRequests");
            DropTable("dbo.LeaveManagementSummaries");
            DropTable("dbo.ReportManagers");
            DropTable("dbo.LeaveManagements");
            DropTable("dbo.Jobtitles");
            DropTable("dbo.IdentityCodes");
            DropTable("dbo.Files");
            DropTable("dbo.EmploymentStatus");
            DropTable("dbo.EmployeeLeaves");
            DropTable("dbo.EmpHolidays");
            DropTable("dbo.CareerPaths");
            DropTable("dbo.Careers");
            DropTable("dbo.AppraisalRatings");
            DropTable("dbo.AppraisalStatus");
            DropTable("dbo.Locations");
            DropTable("dbo.Levels");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.BusinessUnits");
            DropTable("dbo.Questions");
            DropTable("dbo.AppraisalQuestions");
            DropTable("dbo.AppraisalPeriods");
            DropTable("dbo.AppraisalModes");
            DropTable("dbo.Groups");
            DropTable("dbo.AppraisalInitializations");
            DropTable("dbo.AppraisalConfigurations");
            DropTable("dbo.ActivityLogs");
        }
        //public override void Up()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
