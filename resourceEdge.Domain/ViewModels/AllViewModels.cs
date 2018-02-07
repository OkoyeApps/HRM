using resourceEdge.Domain.Infrastructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace resourceEdge.Domain.Entities
{
    public class IDentityViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int codeId { get; set; }
        [Required(ErrorMessage = "Please Provide Employee Code"), StringLength(6, ErrorMessage = "Code should not be more than 6")]
        [Display(Name = "Employee Code")]
        public string employee_code { get; set; }

        [Required(ErrorMessage = "Please Provide Background agency code")]
        [StringLength(6, ErrorMessage = "Code should not be more than 6")]
        [Display(Name = "Background agency Code")]
        public string backgroundagency_code { get; set; }

        [Required(ErrorMessage = "Please Provide Vendor Code")]
        [StringLength(6, ErrorMessage = "Code should not be more than 6")]
        [Display(Name = "Vendor Code")]
        public string vendors_code { get; set; }

        [Required(ErrorMessage = "Please Provide Staff Code")]
        [StringLength(6, ErrorMessage = "Code should not be more than 6")]
        [Display(Name = "Staff Code")]
        public string staffing_code { get; set; }

        [Required(ErrorMessage = "Please Provide user Code")]
        [StringLength(6, ErrorMessage = "Code should not be more than 6")]
        [Display(Name = "user Code")]
        public string users_code { get; set; }

        [Required(ErrorMessage = "Please Provide Requisition Code")]
        [StringLength(6, ErrorMessage = "Code should not be more than 6")]
        [Display(Name = "Requisition Code")]
        public string requisition_code { get; set; }
    }
    public class prefixViewModel
    {
        [HiddenInput(DisplayValue = true)]
        public int prefixId { get; set; }

        [Required(ErrorMessage = "please please provide This Field")]
        [StringLength(6, ErrorMessage = "Length should be less than six(6)", MinimumLength = 2)]
        public string prefixName { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(150, ErrorMessage = "Text should be less than fifty(50) characters")]
        public string description { get; set; }
    }

    public class JobtitlesVIewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int JobId { get; set; }

        [Required(ErrorMessage = "please specify a job title code")]
        [StringLength(15, ErrorMessage = "Length should be less than 15")]
        public string jobtitlecode { get; set; }

        [Required(ErrorMessage = "please specify a job title name")]
        [StringLength(20, ErrorMessage = "Length should be less than Twenty(20)")]
        public string jobtitlename { get; set; }

        [Required(ErrorMessage = "please specify a job description")]
        [StringLength(50, ErrorMessage = "Length should be less than 50")]
        public string jobdescription { get; set; }

        [Required(ErrorMessage = "please specify a Minimum job requirment")]
        public Nullable<double> minexperiencerequired { get; set; }

        [Required(ErrorMessage = "please specify a job Grade")]
        [StringLength(10, ErrorMessage = "Length should be less than 10"), Display(Name = "Pay Code")]
        public string jobpaygradecode { get; set; }

        [Required(ErrorMessage = "please specify a pay frequency")]
        [StringLength(20, ErrorMessage = "Length should be less than 20"), Display(Name = "Pay Frequency")]
        public string jobpayfrequency { get; set; }

        [StringLength(50, ErrorMessage = "Length should be less than Fifty(50)"), Display(Name = " Comments"), DataType(DataType.MultilineText)]
        public string comments { get; set; }
    }

    public class BusinessUnitsVIewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int BusId { get; set; }

        [Required(ErrorMessage = "Please specify Unit name")]
        [StringLength(50, ErrorMessage = "the Name Exceeds 50 characters or is less than 5 characters", MinimumLength = 5), Display(Name = "Unit name")]
        public string unitname { get; set; }

        [Required(ErrorMessage = "please specify unit code")]
        [StringLength(15, ErrorMessage = "Code exceeds 15 characters", MinimumLength = 2), Display(Name = "Unit code")]
        public string unitcode { get; set; }

        [StringLength(100, ErrorMessage = "description exceeds 100 characters")]
        [DataType(DataType.MultilineText), Display(Name = "Description")]
        public string descriptions { get; set; }

        [Required(ErrorMessage = "Please speciffy the state of th current business unit")]
        [Display(Name = "State")]
        [StringLength(20, ErrorMessage = "State name exceeds 20 characters", MinimumLength = 2)]
        public string CurrentState { get; set; }

        [Required(ErrorMessage = "Please fil the country field")]
        [StringLength(20, ErrorMessage = "Country name exceeds 20 characters", MinimumLength = 3), Display(Name = "Country")]
        public string country { get; set; }

        [Required(ErrorMessage = "please specify the city name")]
        [StringLength(20, ErrorMessage = "city value exceeds 20 characters or is less tha 3", MinimumLength = 3), Display(Name = "City")]
        public string city { get; set; }

        [Required(ErrorMessage = "please enter an address")]
        [StringLength(100, ErrorMessage = "address value exceeds 100 characters"), Display(Name = "Address 1")]
        public string address1 { get; set; }
        [Display(Name = "Address 2")]
        public string address2 { get; set; }
        [Display(Name = "Address 3")]
        public string address3 { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> startdate { get; set; }
    }

    public class DeptViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int DeptId { get; set; }
        [Required(ErrorMessage = "please Provide Department name")]
        [StringLength(50, ErrorMessage = "length exceeds 50 characters"), Display(Name = "Department name")]
        public string deptname { get; set; }

        [Required(ErrorMessage = "please provide department code")]
        [StringLength(10, ErrorMessage = "length exceeds 10 characters or is less than 3", MinimumLength = 3), Display(Name = "Department Code")]
        public string deptcode { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(50, ErrorMessage = "length exceeds 50 characters")]
        public string descriptions { get; set; }

        [Required(ErrorMessage = "please specify the Country name"), Display(Name = "Country"), StringLength(20, ErrorMessage = "Name exceeds 20 characters or less than 5", MinimumLength = 5)]
        public string country { get; set; }

        [Required(ErrorMessage = "please specify the State")]
        [Display(Name = "State"), StringLength(20, ErrorMessage = "Exceeds 20 characters or less than 3", MinimumLength = 3)]
        public string CurrentState { get; set; }

        [Required(ErrorMessage = "please specify the City")]
        [Display(Name = "City"), StringLength(20, ErrorMessage = "Exceeds 20 characters or less than 3", MinimumLength = 3)]
        public Nullable<int> city { get; set; }

        [Required(ErrorMessage = "Please provide atleast address1 "), Display(Name = "Address 1")]
        public string address1 { get; set; }
        [Display(Name = "Address 1")]
        public string address2 { get; set; }
        [Display(Name = "Address 1")]
        public string address3 { get; set; }
        public Nullable<int> BunitId { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> createddate { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> startdate { get; set; }
    }
    public class positionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PosId { get; set; }

        [Required(ErrorMessage = "position name is required")]
        [StringLength(20, ErrorMessage = "length exceeds 20 characters"), Display(Name = "Name")]
        public string positionname { get; set; }

        [Required(ErrorMessage = "please select a job specific to the position"), Display(Name = "Job Title")]
        public Nullable<int> jobtitleid { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(50, ErrorMessage = "length exceeds 50 characters"), Display(Name = "Description")]
        public string description { get; set; }

    }

    public class employeeStatusViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int empstId { get; set; }

        [Required(ErrorMessage = "Please enter a name to indicate the employement status"), StringLength(50, ErrorMessage = "length greater than 20"), Display(Name = "Status name")]
        public string employemnt_status { get; set; }
    }

    public class EmployeeViewModel
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        public int empID { get; set; }
        [Required]
        public string identityCode { get; set; }

        [Required(ErrorMessage = "Please provide an Id for the employee"), Display(Name = "Employee Id")]
        public string empUserId { get; set; }

        [Required(ErrorMessage = "Select an employee Role"), Display(Name = "Role")]
        public int empRoleId { get; set; }

        [Required(ErrorMessage = "Please Input First name"), StringLength(30, ErrorMessage = "length exceeds 30 or is less than 3", MinimumLength = 3), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Input Last name"), StringLength(30, ErrorMessage = "length exceeds 30", MinimumLength = 2), Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Please provide an email address"), Display(Name = "Email")]
        [EmailAddress]
        public string empEmail { get; set; }
        [Required(ErrorMessage = "please specify a date for joining"), Display(Name = "Date of Joining"), DataType(DataType.Date)]
        public Nullable<System.DateTime> dateOfJoining { get; set; }
        [Display(Name = "Date of Leaving"), DataType(DataType.Date)]
        public Nullable<System.DateTime> dateOfLeaving { get; set; }

        [Required(ErrorMessage = "please specify the status field"), Display(Name = "Employment Status")]
        public string empStatusId { get; set; }
        [Required(ErrorMessage = "please select a business unit"), Display(Name = "Business Unit")]
        public Nullable<int> businessunitId { get; set; }
        [Required(ErrorMessage = "please select a department"), Display(Name = "Department")]
        public Nullable<int> departmentId { get; set; }
        [Required(ErrorMessage = "please select a Job"), Display(Name = "Job Title")]
        public Nullable<int> jobtitleId { get; set; }
        [Required(ErrorMessage = "please select a Position"), Display(Name = "Position ")]
        public Nullable<int> positionId { get; set; }
        [Display(Name = "Years of Experience")]
        public string yearsExp { get; set; }
        [Required(ErrorMessage = "please specify a prefix"), Display(Name = "Prefix")]
        public prefixes prefixId { get; set; }
        [Required(ErrorMessage = "Please specify a Phone number"), Display(Name = "Phone Number")]
        public string officeNumber { get; set; }

        [Required(ErrorMessage = "please select a mode of entry")]
        public ModeOfEmployement modeofEmployement { get; set; }
    }

    public class LeaveManagementViewModel
    {
        [Key, HiddenInput(DisplayValue = false)]
        public int id { get; set; }
        [Required(ErrorMessage = "Please select a calender Start month"), Display(Name = "Calendar Start Month")]
        public Nullable<int> CalStartMonth { get; set; }
        [Required(ErrorMessage = "Please select a Weekend start day"), Display(Name = "Weekend day 1")]
        public Nullable<int> WeekendStartDay { get; set; }
        [Required(ErrorMessage = "Please select a Weekend end day"), Display(Name = "Weekend day 2")]
        public Nullable<int> WeekendEndDay { get; set; }
        [Required(ErrorMessage = "Please select a Business Unit"), Display(Name = "Business Unit")]
        public int? businessunitId { get; set; }
        [Required(ErrorMessage = "Please select Department"), Display(Name = "Department")]
        public string departmentId { get; set; }
        [Required(ErrorMessage = "Please select a HR Manager"), Display(Name = "Hr Manager")]
        public string HrId { get; set; }
        [Required(ErrorMessage = "Please input Working Hours"), Display(Name = "Working Hours")]
        public Nullable<int> HoursDay { get; set; }

        [Required(ErrorMessage = "Please select Half Day Requests"), Display(Name = "Half Day Requests")]
        public Answers IsHalfday { get; set; }
        [Required(ErrorMessage = "Allow Leave Transfers? not specified"), Display(Name = "Allow Leave Transfers?")]
        public Answers IsLeaveTransfer { get; set; }
        [Required(ErrorMessage = "Skip Holidays? not specified"), Display(Name = "Skip Holidays?")]
        public Answers IsSkipHolidays { get; set; }
        [Required(ErrorMessage = "Please select a calender Start month"), Display(Name = "Description")]
        public string Descriptions { get; set; }
        public Nullable<bool> Isactive { get; set; }
    }

    public class reportmanagerViewModel
    {
        
        [Required(ErrorMessage ="Please select an Employee ")]
        public string userId { get; set; }
        [Required(ErrorMessage ="please select a Business unit")]
        public string BunitId { get; set; }
        public string empID { get; set; }
    }

    public class LeaveTypeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = " please specify leave name"), Display(Name = "Leave Type")]
        public string leavetype { get; set; }
        [Required(ErrorMessage = " please specify Number of days Available"), Display(Name = "Number of Days")]
        public Nullable<int> numberofdays { get; set; }
        [Required(ErrorMessage = " please specify leave Code"), Display(Name = "Leave Code")]
        public string leavecode { get; set; }
        [Display(Name = "Description"), StringLength(100, ErrorMessage ="length exceeds 20 characters"), DataType(DataType.MultilineText)]
        public string description { get; set; }
        public Answers leavepreallocated { get; set; }
    }

    public class LeaveRequestViewModel
    {
        [Key, HiddenInput(DisplayValue =false)]
        public int MyProperty { get; set; }
        public string UserId { get; set; }
        public string AvailableLeave { get; set; }
        [Required(ErrorMessage ="please state reason for your Request"), Display(Name ="Reason")]
        public string Reason { get; set; }
        [Required(ErrorMessage ="Please select a type of leave"), Display(Name ="Leave Type")]
        public Nullable<int> LeavetypeId { get; set; }
        [Display(Name ="From"), Required(ErrorMessage ="Please select a Start Date"), DataType(DataType.Date)]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Display(Name = "To"), Required(ErrorMessage = "Please select a End Date"), DataType(DataType.Date)]
        public Nullable<System.DateTime> ToDate { get; set; }
        [Required(ErrorMessage ="Sorry No Reporting Manager for you yet")]
        public string RepmangId { get; set; }
        public int requestDays { get; set; }
        public Nullable<double> LeaveNoOfDays { get; set; }
    }

    public class RequisitionViewModel
    {
        [Key, HiddenInput(DisplayValue = false)]
        public int id { get; set; }
        [Required(ErrorMessage ="Requisition Code not configured"),Display(Name ="Requisition Code")]
        public string RequisitionCode { get; set; }
        [Required(ErrorMessage = "Please select an Due Date"), Display(Name = "Due Date ")]
        public Nullable<System.DateTime> OnboardDate { get; set; }
        [Required(ErrorMessage = "Please select a Position"), Display(Name = "Position")]
        public Nullable<int> PositionId { get; set; }
        [Required(ErrorMessage ="Please select a Reporting manager"), Display(Name = "Reporting Manager")]
        public string ReportingId { get; set; }
        [Required(ErrorMessage ="Please select a businesss unit"), Display(Name = "Business Unit")]
        public Nullable<int> BusinessunitId { get; set; }
        [Required(ErrorMessage = "Please select a Department"), Display(Name = "Department")]
        public Nullable<int> DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select a Job"), Display(Name = "Job")]
        public Nullable<int> JobTitle { get; set; }
        [Required(ErrorMessage = "Please select a Position"), Display(Name = "Position")]
        public string ReqNoPositions { get; set; }

        public Nullable<int> SelectedMembers { get; set; }
        public Nullable<int> FilledPositions { get; set; }

        public string JobDescription { get; set; }
        [Required(ErrorMessage ="Required skills must be provided"), Display(Name ="Required Skills")]
        public string ReqSkills { get; set; }
        [Required(ErrorMessage = "Please provide a qualification"), Display(Name = "Required Qualification")]
        public string ReqQualification { get; set; }
        [Required(ErrorMessage ="Please provide an experience range"), Display(Name ="Experience Range")]
        public string ReqExpYears { get; set; }
        [Required(ErrorMessage ="Please select employment status"), Display(Name ="Employment Status")]
        public string EmpType { get; set; }
        [Required(ErrorMessage ="Please select the Priority"), Display(Name ="Priority")]
        public Priority ReqPriority { get; set; }
        public string AdditionalInfo { get; set; }
        public string ReqStatus { get; set; }
        [Required(ErrorMessage ="Approval One is Required"), Display(Name ="Approval-1")]
        public string Approver1 { get; set; }
        public string Approver2 { get; set; }
        public string Approver3 { get; set; }
        public string AppStatus1 { get; set; }
        public string AppStatus2 { get; set; }
        public string AppStatus3 { get; set; }
        [Display(Name ="Client")]
        public string ClientId { get; set; }

    }
}
