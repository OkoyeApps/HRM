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
            var BusinessUnit = new List<BusinessUnits>()
            {
                new BusinessUnits()
                {
                unitname = "TestUnit1", unitcode = "Test111",
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now,
                },

                new BusinessUnits()
                {
                 unitname = "TestUnit2", unitcode = "Test111",
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now,
               },

                new BusinessUnits()
                {
                unitname = "TestUnit3", unitcode = "Test111",
                descriptions = "Tesing Business Unit Description",  isactive = true,startdate = DateTime.Now,

                }

            };
            foreach (var item in BusinessUnit)
            {
                context.GetDbContext().businessunits.Add(item);
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
                context.GetDbContext().departments.Add(item);
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
                context.GetDbContext().jobtitles.Add(item);
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
                context.GetDbContext().positions.Add(item);
                context.Save();
            }
            var employmentStatus = new List<EmploymentStatus>()
            {
                new EmploymentStatus() { employemntStatus = "Permanent"  },
                new EmploymentStatus() { employemntStatus = "Temporary" }
            };
            foreach (var item in employmentStatus)
            {
                context.GetDbContext().employmentstatus.Add(item);
                context.Save();
            }
            var identityCode = new IdentityCodes()
            {
                employee_code = "Tenece",
                backgroundagency_code = "Bck",
                requisition_code = "Req",
                staffing_code = "TenStf",
                users_code = "User",
                vendors_code = "Ven"
            };
            context.identityCodes.Insert(identityCode);
            context.Save();
            var appUser = new ApplicationUser() { Email = "Admin@example.com", UserName = "Admin@example.com" };

            var result = userManager.Create(appUser, "1234567");
            //add the user to role later
            if (result.Succeeded)
            {
                userManager.AddToRole(appUser.Id, "System Admin");
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
        }
    }
    // base.Seed(context);
}
