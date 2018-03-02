using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using resourceEdge.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.webUi.Models;
using resourceEdge.Domain.UnitofWork;
using Microsoft.AspNet.Identity;
using resourceEdge.webUi.Security;

namespace resourceEdge.webUi.Infrastructure
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<EdgeDbContext>
    {
        protected override void Seed(EdgeDbContext dbcontext)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            UnitOfWork context = new UnitOfWork();

            var group = new List<Groups>()
            {

                new Groups() { GroupName = "Tenece", CreatedDate = DateTime.Now },
                new Groups() { GroupName = "Genesys", CreatedDate = DateTime.Now },
                new Groups() {GroupName = "Piewa", CreatedDate = DateTime.Now }
            };
            group.ForEach(X => context.Groups.Insert(X));
            var Locations = new List<Location>()
            {
                new Location()
                {
                 GroupId = 1, Country = "Nigeria", City = "Enugu", State = "Enugu", Address1 = "Centinary City",
                 CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now
                },
                 new Location()
                 {
                 GroupId = 1, Country = "Nigeria", City = "Lagos", State = "Lagos", Address1 = "Centinary City",
                 CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now
                 }
            };
            Locations.ForEach(x => context.Locations.Insert(x));
            context.Save();
            var BusinessUnit = new List<BusinessUnits>()
            {
                new BusinessUnits()
                {
                unitname = "TestUnit1", unitcode = "Test111",
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now, LocationId = 1
                },

                new BusinessUnits()
                {
                 unitname = "TestUnit2", unitcode = "Test111",
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now,LocationId = 1
               },

                new BusinessUnits()
                {
                unitname = "TestUnit3", unitcode = "Test111",
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now,LocationId = 1

                }

            };
            foreach (var item in BusinessUnit)
            {
                context.BusinessUnit.Insert(item);
                context.Save();
            }
            var department = new List<Departments>()
            {
                new Departments()
                {
                     deptname = "TestDept",deptcode = "Test101",BunitId = 1,
                      Isactive = true, startdate = DateTime.Now
                },
                   new Departments()
                {
                     deptname = "TestDept2",deptcode = "Test101", BunitId = 1,
                     Isactive = true, startdate = DateTime.Now
                },
                      new Departments()
                {
                     deptname = "TestDept3", deptcode = "Test101", BunitId = 2 ,Isactive = true, startdate = DateTime.Now
                }
            };
            foreach (var item in department)
            {
                context.Department.Insert(item);
                context.Save();
            }
            var jobs = new List<Jobtitles>()
            {
                new Jobtitles()
                {
                    jobtitlename = "TestJob", jobtitlecode = "JobT", jobpayfrequency ="Monthly", jobpaygradecode ="A",jobdescription = "Test Job description",
                    minexperiencerequired = 2, isactive = true, comments = "Testing Job"
                },
                   new Jobtitles()
                {
                    jobtitlename = "TestJob2", jobtitlecode = "JobT", jobpayfrequency ="Monthly", jobpaygradecode ="A",jobdescription = "Test Job description",
                    minexperiencerequired = 2, isactive = true, comments = "Testing Job"
                },
                      new Jobtitles()
                {
                    jobtitlename = "TestJob3", jobtitlecode = "JobT", jobpayfrequency ="Monthly", jobpaygradecode ="A", jobdescription = "Test Job description",
                    minexperiencerequired = 2, isactive = true, comments = "Testing Job"
                }
            };
            foreach (var item in jobs)
            {
                context.jobTitles.Insert(item);
                context.Save();
            }

            var position = new List<Positions>()
            {
                new Positions() { positionname = "TestPosi", jobtitleid = 1, isactive =true },
                new Positions() { positionname = "TestPosi", jobtitleid = 1, isactive =true },
                new Positions() { positionname = "TestPosi", jobtitleid = 2, isactive =true },
                new Positions() { positionname = "TestPosi", jobtitleid = 2, isactive =true }
            };
            foreach (var item in position)
            {
                context.positions.Insert(item);
                context.Save();
            }
            var employmentStatus = new List<EmploymentStatus>()
            {
                new EmploymentStatus() { employemntStatus = "Permanent"  },
                new EmploymentStatus() { employemntStatus = "Temporary" }
            };
            foreach (var item in employmentStatus)
            {
                context.employmentStatus.Insert(item);
                context.Save();
            }
            var level = new List<Levels>()
            {
                new Levels() { LevelName ="Beginner", EligibleYears = 3, levelNo = 1, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now },
                new Levels() { LevelName ="Professional", EligibleYears = 7, levelNo = 8, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now }
            };
            level.ForEach(x => context.Levels.Insert(x));
            var identityCode = new IdentityCodes()
            {
                employee_code = "Tenece",
                backgroundagency_code = "Bck",
                requisition_code = "Req",
                staffing_code = "TenStf",
                users_code = "User",
                vendors_code = "Ven",
                 GroupId = 1
            };
            context.identityCodes.Insert(identityCode);
            context.Save();
            var appUser = new ApplicationUser() { Email = "Admin@example.com", UserName = "Admin@example.com" };

            var result = userManager.Create(appUser, "1234567");
            //add the user to role later
            if (result.Succeeded)
            {
                userManager.AddToRole(appUser.Id, "System Admin");
                userManager.AddToRole(appUser.Id, "Head HR");
            }

            var TestUser1 = new List<ApplicationUser>()
            {
                new ApplicationUser() { Email = "Test1@example.com", UserName = "Test1@example.com" },
                new ApplicationUser() {Email = "Hr@example.com", UserName = "Hr@example.com" },
                new ApplicationUser() {Email = "Manager@example.com", UserName = "Manager@example.com" },
                new ApplicationUser() {Email = "DeptHead@example.com", UserName = "DeptHead@example.com" }
            };
            var Employees = new Employees[]
            {
                new Domain.Entities.Employees() { businessunitId = 1, departmentId = 2, empEmail = "Test1@example.com",
                    empRoleId = 4, empStatusId = "Test User", GroupId = 1, LevelId = 1, FullName = "Test User",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, isactive = true   },
                new Domain.Entities.Employees() { businessunitId = 1, departmentId = 2, empEmail = "Hr@example.com",
                    empRoleId = 3, empStatusId = "Test Hr", GroupId = 1, LevelId = 1,FullName = "Test HR",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, isactive = true   },
                new Domain.Entities.Employees() { businessunitId = 1, departmentId = 2, empEmail = "Manager@example.com",
                    empRoleId = 2, empStatusId = "Test User", GroupId = 1, LevelId = 1,FullName = "Test Manager",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, isactive = true   },
                  new Domain.Entities.Employees() { businessunitId = 1, departmentId = 2, empEmail = "DeptHead@example.com",
                    empRoleId = 2, empStatusId = "Test User", GroupId = 1, LevelId = 1,FullName = "Test Dept",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, IsDepthead = true, isactive = true   },
            };
            for (int i = 0, j = 0; i < TestUser1.Count; i++,j++)
            {
                var result2 = userManager.Create(TestUser1[i], "1234567");
                if (result.Succeeded)
                {
                    if (TestUser1[i].Email.StartsWith("Test"))
                    {
                        userManager.AddToRole(TestUser1[i].Id, "Employee");
                    }
                    else if (TestUser1[i].Email.StartsWith("Hr"))
                    {
                        userManager.AddToRole(TestUser1[i].Id, "HR");
                    }
                    else if (TestUser1[i].Email.StartsWith("Manager"))
                    {
                        userManager.AddToRole(TestUser1[i].Id, "Manager");
                    }
                }
                Employees[i].userId = TestUser1[i].Id;
                context.employees.Insert(Employees[j]);
             }
            context.Save();
            var MonthsList = new List<MonthList>()
            {
            new MonthList() {  MonthId = "1", MonthCode = "Jan", Description = "January", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "2", MonthCode = "Feb", Description = "February", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "3", MonthCode = "Mar", Description = "March", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "4", MonthCode = "April", Description = "April", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "5", MonthCode = "May", Description = "May", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "6", MonthCode = "June", Description = "June", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "7", MonthCode = "July", Description = "July", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "8", MonthCode = "Aug", Description = "Aug", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "9", MonthCode = "Sep", Description = "September", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "10", MonthCode = "Oct", Description = "October", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "11", MonthCode = "Nov", Description = "November", Createdby = "1", Modifiedby = "1", Isactive = true },
            new MonthList() {  MonthId = "12", MonthCode = "Dec", Description = "December", Createdby = "1", Modifiedby = "1", Isactive = true }
            };
            foreach (var item in MonthsList)
            {
                context.GetDbContext().MonthList.Add(item);
                context.Save();
            }

            var Months = new List<Months>()
            {
                new Months() { MonthId = "1", MonthName = "January", Isactive = true, },
                new Months() { MonthId = "2", MonthName = "February", Isactive = true },
                new Months() { MonthId = "3", MonthName = "March", Isactive = true },
                new Months() { MonthId = "4", MonthName = "April", Isactive = true },
                new Months() { MonthId = "5", MonthName = "May", Isactive = true },
                new Months() { MonthId = "6", MonthName = "June", Isactive = true },
                new Months() { MonthId = "7", MonthName = "July", Isactive = true },
                new Months() { MonthId = "8", MonthName = "August", Isactive = true },
                new Months() { MonthId = "9", MonthName = "September", Isactive = true },
                new Months() { MonthId = "10", MonthName = "October", Isactive = true },
                new Months() { MonthId = "11", MonthName = "November", Isactive = true },
                new Months() { MonthId = "12", MonthName = "December", Isactive = true }
            };
            foreach (var item in Months)
            {
                context.GetDbContext().Months.Add(item);
                context.Save();
            }

            var WeekDays = new List<WeekDays>()
            {
                      new WeekDays() {  DayName =  "1", DayShortCode =  "Mo",DayLongCode =  "Mon", description = "Monday", Isactive = true },
                      new WeekDays() { DayName =  "2", DayShortCode =  "Tu", DayLongCode =  "Tue", description =  "Tueday", CreatedBy =  "1", Isactive = true },
                      new WeekDays() {  DayName =  "3", DayShortCode =  "We", DayLongCode =  "Wed", description =  "Wednesday", CreatedBy =  "1", Isactive = true },
                      new WeekDays() {  DayName =  "4", DayShortCode =  "Th", DayLongCode =  "Thu", description =  "Thursday", CreatedBy =  "1", Isactive = true },
                      new WeekDays() {  DayName =  "5", DayShortCode =  "Fr", DayLongCode =  "Fri", description =  "Friday", CreatedBy =  "1", Isactive = true },
                      new WeekDays() {  DayName =  "6", DayShortCode =  "Sa", DayLongCode =  "Sat", description =  "Saturday", CreatedBy =  "1", Isactive = true },
                      new WeekDays() {  DayName =  "7", DayShortCode =  "Su", DayLongCode =  "Sun", description =  "Sunday", CreatedBy =  "1", Isactive = true }
            };
            foreach (var item in WeekDays)
            {
                context.GetDbContext().WeekDays.Add(item);
                context.Save();
            }


            var weeks = new List<Weeks>()
            {
                new Weeks() {  WeekId = "1", WeekName = "Sunday", Isactive = true  },
                 new Weeks() {  WeekId = "2", WeekName = "Monday", Isactive = true  },
                  new Weeks() {  WeekId = "3", WeekName = "Tuesday", Isactive = true  },
                   new Weeks() {WeekId = "4", WeekName = "Wednesday", Isactive = true  },
                    new Weeks() {  WeekId = "5", WeekName = "Thursday", Isactive = true  },
                     new Weeks() {  WeekId = "6", WeekName = "Friday", Isactive = true  },
                     new Weeks() { WeekId = "7", WeekName = "Saturday", Isactive = true  }
            };
            foreach (var item in weeks)
            {
                context.GetDbContext().Weeks.Add(item);
                context.Save();
            }

            var AppraisalStatus = new List<AppraisalStatus>()
            {
                 new Domain.Entities.AppraisalStatus() { Name = "Open" },
                 new Domain.Entities.AppraisalStatus() { Name = "Closed" },
                 new Domain.Entities.AppraisalStatus() { Name = "InProgress" }
            };
            foreach (var item in AppraisalStatus)
            {
                context.AppraisalStatus.Insert(item);
                context.Save();
            }
            var AppraisalMode = new List<AppraisalMode>()
            {
                new Domain.Entities.AppraisalMode() { Name= "Quarterly" },
                 new Domain.Entities.AppraisalMode() { Name = "Half-Year" },
                 new Domain.Entities.AppraisalMode() {Name = "Yearly" }
            };
            foreach (var item in AppraisalMode)
            {
                context.AppraisalMode.Insert(item);
                context.Save();
            }
            var AppraisalRating = new List<AppraisalRating>()
            {
                new Domain.Entities.AppraisalRating() { Name = "1-5" },
                new Domain.Entities.AppraisalRating() { Name = "1-10" }
            };
            foreach (var item in AppraisalRating)
            {
                context.AppraislRating.Insert(item);
                context.Save();
            }
            var periods = new List<AppraisalPeriods>()
            {
                new AppraisalPeriods() { Name = "Q1" },
                new AppraisalPeriods() {Name = "H1" },
                new AppraisalPeriods() {Name = "Yearly" }
            };
            periods.ForEach(X => context.AppraisalPeriod.Insert(X));
            var parameter = new List<Parameters>()
            {
                new Parameters() { ParameterName = "KPI", Descriptions = "Key Point Index" }
            };
            parameter.ForEach(X => context.Parameters.Insert(X));
            context.Save();
        }
    }
    // base.Seed(context);
}
