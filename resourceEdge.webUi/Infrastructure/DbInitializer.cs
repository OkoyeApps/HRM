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
using System.Data.Entity.Migrations;

namespace resourceEdge.webUi.Infrastructure
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<EdgeDbContext>
    {
        protected override void Seed(EdgeDbContext dbcontext)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            UnitOfWork context = new UnitOfWork();
           
            var groups = new List<Group>()
            {
                new Group() { GroupName = "Tenece", CreatedDate = DateTime.Now },
                new Group() { GroupName = "Genesys", CreatedDate = DateTime.Now },
                new Group() {GroupName = "Piewa", CreatedDate = DateTime.Now }
            };
            foreach (var group in groups)
            {
                context.GetDbContext().Group.AddOrUpdate(group);
                context.Save();
            }
            //group.ForEach(X => context.GetDbContext().Group.AddOrUpdate(X));
            //context.Save();
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
            foreach (var local in Locations)
            {
                context.GetDbContext().Location.AddOrUpdate(local);
                context.Save();
            }
            var BusinessUnit = new List<BusinessUnit>()
            {
                new BusinessUnit()
                {
                unitname = "TestUnit1", unitcode = "Test111", GroupId = 1,
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now, LocationId = 1
                },

                new BusinessUnit()
                {
                 unitname = "TestUnit2", unitcode = "Test111", GroupId =1,
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now,LocationId = 1
               },

                new BusinessUnit()
                {
                unitname = "TestUnit3", unitcode = "Test111", GroupId = 1,
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now,LocationId = 1

                }

            };
            foreach (var item in BusinessUnit)
            {
                context.GetDbContext().Businessunit.AddOrUpdate(item);
                context.Save();
            }
            var department = new List<Departments>()
            {
                new Departments()
                {
                     deptname = "TestDept",deptcode = "Test101",BusinessUnitsId = 1,
                      Isactive = true, startdate = DateTime.Now
                },
                   new Departments()
                {
                     deptname = "TestDept2",deptcode = "Test101", BusinessUnitsId = 1,
                     Isactive = true, startdate = DateTime.Now
                },
                      new Departments()
                {
                     deptname = "TestDept3", deptcode = "Test101", BusinessUnitsId = 2 ,Isactive = true, startdate = DateTime.Now
                }
            };
            foreach (var item in department)
            {
                context.GetDbContext().departments.AddOrUpdate(item);
                context.Save();
            }
            var jobs = new List<Jobtitle>()
            {
                new Jobtitle()
                {
                    jobtitlename = "TestJob", jobtitlecode = "JobT", jobpayfrequency ="Monthly", jobpaygradecode ="A",jobdescription = "Test Job description",
                    minexperiencerequired = 2, isactive = true, comments = "Testing Job", GroupId = 1
                },
                   new Jobtitle()
                {
                    jobtitlename = "TestJob2", jobtitlecode = "JobT", jobpayfrequency ="Monthly", jobpaygradecode ="A",jobdescription = "Test Job description",
                    minexperiencerequired = 2, isactive = true, comments = "Testing Job",GroupId = 1
                },
                      new Jobtitle()
                {
                    jobtitlename = "TestJob3", jobtitlecode = "JobT", jobpayfrequency ="Monthly", jobpaygradecode ="A", jobdescription = "Test Job description",
                    minexperiencerequired = 2, isactive = true, comments = "Testing Job",GroupId = 1
                }
            };
            //foreach (var item in jobs)
            //{
            //    context.GetDbContext().Jobtitle.AddOrUpdate(item);
            //}
            jobs.ForEach(x => context.jobTitles.Insert(x));
            context.Save();

            var position = new List<Position>()
            {
                new Position() { positionname = "TestPosi", JobtitleId = 1, isactive =true },
                new Position() { positionname = "TestPosi", JobtitleId = 1, isactive =true },
                new Position() { positionname = "TestPosi", JobtitleId = 2, isactive =true },
                new Position() { positionname = "TestPosi", JobtitleId = 2, isactive =true }
            };
            foreach (var item in position)
            {
                context.GetDbContext().Position.AddOrUpdate(item);
                context.Save();
            }
            var employmentStatus = new List<EmploymentStatus>()
            {
                new EmploymentStatus() { employemntStatus = "Permanent"  },
                new EmploymentStatus() { employemntStatus = "Temporary" }
            };
            foreach (var item in employmentStatus)
            {
                context.GetDbContext().EmploymentStatus.AddOrUpdate(item);
                context.Save();
            }
            var levels = new List<Level>()
            {
                new Level() { LevelName ="Beginner", EligibleYears = 3, GroupId = 1, levelNo = 1, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now },
                new Level() { LevelName ="Professional", EligibleYears = 7, GroupId = 1, levelNo = 8, CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now }
            };
            foreach (var level in levels)
            {
                context.GetDbContext().Level.AddOrUpdate(level);
                context.Save();
            }
            //level.ForEach(x => context.GetDbContext().Level.AddOrUpdate(x));
            //context.Save();
            var identityCode = new IdentityCode()
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
                new ApplicationUser() {Email = "DeptHead@example.com", UserName = "DeptHead@example.com" },
                new ApplicationUser() {Email = "LocationHead@example.com", UserName = "LocationHead@example.com" }
            };
            var Employee = new Employee[]
            {
                new Domain.Entities.Employee() { businessunitId = 1, DepartmentId = 2, empEmail = "Test1@example.com",
                    empRoleId = 4, empStatusId = "Test User", GroupId = 1, LevelId = 1, FullName = "Test User",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, isactive = true   },
                new Domain.Entities.Employee() { businessunitId = 1, DepartmentId = 2, empEmail = "Hr@example.com",
                    empRoleId = 3, empStatusId = "Test Hr", GroupId = 1, LevelId = 1,FullName = "Test HR",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, isactive = true   },
                new Domain.Entities.Employee() { businessunitId = 1, DepartmentId = 2, empEmail = "Manager@example.com",
                    empRoleId = 2, empStatusId = "Test User", GroupId = 1, LevelId = 1,FullName = "Test Manager",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, isactive = true   },
                  new Domain.Entities.Employee() { businessunitId = 1, DepartmentId = 2, empEmail = "DeptHead@example.com",
                    empRoleId = 2, empStatusId = "Test User", GroupId = 1, LevelId = 1,FullName = "Test Dept",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, IsDepthead = true, isactive = true   },
                      new Domain.Entities.Employee() { businessunitId = 1, DepartmentId = 2, empEmail = "LocationHead@example.com",
                    empRoleId = 7, empStatusId = "Test location", GroupId = 1, LevelId = 1,FullName = "Test Location Head",
                    positionId = 1,  LocationId = 1, modeofEmployement = Domain.Infrastructures.ModeOfEmployement.Direct,
                    jobtitleId = 1, isactive = true   }
            };
            for (int i = 0, j = 0; i < TestUser1.Count; i++,j++)
            {
                TestUser1[i].EmployeeId = "Tenece" + i;
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
                        userManager.AddToRole(TestUser1[i].Id, "Head HR");
                    }
                    else if (TestUser1[i].Email.StartsWith("Manager"))
                    {
                        userManager.AddToRole(TestUser1[i].Id, "Manager");
                        var manager = new ReportManager() { BusinessUnitId = Employee[i].businessunitId,
                            DepartmentId = Employee[i].DepartmentId,
                            employeeId = 3,
                            FullName = "Test Manager", ManagerUserId = TestUser1[i].Id
                        };
                        context.ReportManager.Insert(manager);
                    }
                    else if (TestUser1[i].Email.StartsWith("LocationHead"))
                    {
                        userManager.AddToRole(TestUser1[i].Id, "Location Head");
                        var location = context.Locations.GetByID(Employee[i].LocationId);
                        location.LocationHead1 = TestUser1[i].Id;
                        context.Locations.Update(location);
                    }
                }
                Employee[i].userId = TestUser1[i].Id;
                context.GetDbContext().Employee.AddOrUpdate(Employee[j]);
                context.Save();
             }
            
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
                context.GetDbContext().MonthList.AddOrUpdate(item);
                context.Save();
            }

            var Months = new List<Month>()
            {
                new Month() { MonthId = "1", MonthName = "January", Isactive = true, },
                new Month() { MonthId = "2", MonthName = "February", Isactive = true },
                new Month() { MonthId = "3", MonthName = "March", Isactive = true },
                new Month() { MonthId = "4", MonthName = "April", Isactive = true },
                new Month() { MonthId = "5", MonthName = "May", Isactive = true },
                new Month() { MonthId = "6", MonthName = "June", Isactive = true },
                new Month() { MonthId = "7", MonthName = "July", Isactive = true },
                new Month() { MonthId = "8", MonthName = "August", Isactive = true },
                new Month() { MonthId = "9", MonthName = "September", Isactive = true },
                new Month() { MonthId = "10", MonthName = "October", Isactive = true },
                new Month() { MonthId = "11", MonthName = "November", Isactive = true },
                new Month() { MonthId = "12", MonthName = "December", Isactive = true }
            };
            foreach (var item in Months)
            {
                context.GetDbContext().Month.AddOrUpdate(item);
                context.Save();
            }

            var WeekDays = new List<WeekDay>()
            {
                      new WeekDay() {  DayName =  "1", DayShortCode =  "Mo",DayLongCode =  "Mon", description = "Monday", Isactive = true },
                      new WeekDay() { DayName =  "2", DayShortCode =  "Tu", DayLongCode =  "Tue", description =  "Tueday", CreatedBy =  "1", Isactive = true },
                      new WeekDay() {  DayName =  "3", DayShortCode =  "We", DayLongCode =  "Wed", description =  "Wednesday", CreatedBy =  "1", Isactive = true },
                      new WeekDay() {  DayName =  "4", DayShortCode =  "Th", DayLongCode =  "Thu", description =  "Thursday", CreatedBy =  "1", Isactive = true },
                      new WeekDay() {  DayName =  "5", DayShortCode =  "Fr", DayLongCode =  "Fri", description =  "Friday", CreatedBy =  "1", Isactive = true },
                      new WeekDay() {  DayName =  "6", DayShortCode =  "Sa", DayLongCode =  "Sat", description =  "Saturday", CreatedBy =  "1", Isactive = true },
                      new WeekDay() {  DayName =  "7", DayShortCode =  "Su", DayLongCode =  "Sun", description =  "Sunday", CreatedBy =  "1", Isactive = true }
            };
            foreach (var item in WeekDays)
            {
                context.GetDbContext().WeekDay.AddOrUpdate(item);
                context.Save();
            }


            var weeks = new List<Week>()
            {
                new Week() {  WeekId = "1", WeekName = "Sunday", Isactive = true  },
                 new Week() {  WeekId = "2", WeekName = "Monday", Isactive = true  },
                  new Week() {  WeekId = "3", WeekName = "Tuesday", Isactive = true  },
                   new Week() {WeekId = "4", WeekName = "Wednesday", Isactive = true  },
                    new Week() {  WeekId = "5", WeekName = "Thursday", Isactive = true  },
                     new Week() {  WeekId = "6", WeekName = "Friday", Isactive = true  },
                     new Week() { WeekId = "7", WeekName = "Saturday", Isactive = true  }
            };
            foreach (var item in weeks)
            {
                context.GetDbContext().Week.AddOrUpdate(item);
                context.Save();
            }

            var AppraisalStatus = new List<AppraisalStatus>()
            {
                 new Domain.Entities.AppraisalStatus() { Name = "Open" },
                 new Domain.Entities.AppraisalStatus() { Name = "Closed" },
                 new Domain.Entities.AppraisalStatus() { Name = "In-Progress" }
            };
            foreach (var item in AppraisalStatus)
            {
                context.GetDbContext().AppraisalStatus.AddOrUpdate(item);
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
                context.GetDbContext().AppraisalMode.AddOrUpdate(item);
                context.Save();
            }
            var AppraisalRating = new List<AppraisalRating>()
            {
                new Domain.Entities.AppraisalRating() { Name = "1-6" },
                new Domain.Entities.AppraisalRating() { Name = "1-11" },
                new Domain.Entities.AppraisalRating() { Name = "1-4" }
            };
            foreach (var item in AppraisalRating)
            {
                context.GetDbContext().ApprasialRating.AddOrUpdate(item);
                context.Save();
            }
            var periods = new List<AppraisalPeriod>()
            {
                new AppraisalPeriod() { Name = "Q1" },
                new AppraisalPeriod() {Name = "H1" },
                new AppraisalPeriod() {Name = "Yearly" }
            };
            periods.ForEach(X => context.AppraisalPeriod.Insert(X));
            context.Save();
            var parameter = new List<Parameters>()
            {
                new Parameters() { ParameterName = "KPI", Descriptions = "Key Performance Index" }
            };
            foreach (var param in parameter)
            {
                context.GetDbContext().Parameter.AddOrUpdate(param);
                context.GetDbContext().SaveChanges();
            }
            var Menus = new List<Menu>()
            {
                new Domain.Entities.Menu() { Id= 1, Name = "Question", Role = "Manager,HR,System Admin", Active = false },
                new Domain.Entities.Menu() { Id = 2, Name="EmployeeAppraisal", Role =  "Employee, HR,Manager", Active = false }
            };
            Menus.ForEach(x => context.Menu.Insert(x));
            context.Save();
            var candidateStatus = new List<CandidateStatus>()
            {
                new CandidateStatus { Name = "Pending" },
                new CandidateStatus { Name = "Accepted" },
                new CandidateStatus { Name = "Rejected" }
            };
            candidateStatus.ForEach(x=>context.CandidateStatus.Insert(x));
            var interviewStatus = new List<InterviewStatus>()
            {
                new InterviewStatus() { Name = "In-Process" },
                new InterviewStatus { Name = "Finished" }
            };
            interviewStatus.ForEach(x => context.InterviewStatus.Insert(x));

            var interviewType = new List<InterviewType>()
            {
                 new InterviewType { Name = "Online" },
                 new InterviewType { Name = "Written" }
            };
            interviewType.ForEach(x => context.InterviewType.Insert(x));
            context.Save();
        }
    }
}
