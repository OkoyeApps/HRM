using Microsoft.AspNet.Identity.EntityFramework;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.SystemManagers
{
    public class DropDownManager
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public SelectList GetParameter()
        {
            var result = new SelectList(unitOfWork.Parameters.Get().OrderBy(x => x.Id), "Id", "ParameterName", "Id");
            return result;
        }

        public SelectList GetGroup()
        {
            var result = new SelectList(unitOfWork.Groups.Get().OrderBy(X => X.Id), "Id", "GroupName", "Id");
            return result;
        }
        public SelectList GetLocation()
        {
            var result = new SelectList(unitOfWork.Locations.Get().OrderBy(x => x.State), "Id", "State", "Id");
            return result;
        }

        public SelectList GetBusinessUnit(int? groupId = null, int? locationId =null)
        {
            SelectList result = null;
            if (locationId != null)
            {
                 result = new SelectList(unitOfWork.BusinessUnit.Get(filter: x=>x.LocationId ==locationId).OrderBy(x => x.unitname), "Id", "unitname");
            }
            if (locationId != null && groupId != null)
            {
                 result = new SelectList(unitOfWork.BusinessUnit.Get(filter: x => x.LocationId == locationId && x.GroupId ==groupId).OrderBy(x => x.unitname), "Id", "unitname");
            }
            if (groupId != null && locationId == null)
            {
                 result = new SelectList(unitOfWork.BusinessUnit.Get(filter: x => x.GroupId == groupId).OrderBy(x => x.unitname), "Id", "unitname");
            }
             //result = new SelectList(unitOfWork.BusinessUnit.Get().OrderBy(x => x.unitname), "Id", "unitname");
            return result;
        }
       

        public SelectList GetJobtitle(int? groupid=null, int? locationnid = null)
        {
            SelectList result = null;
            if (groupid != null)
            {
                 result = new SelectList(unitOfWork.jobTitles.Get(filter: x=> x.GroupId == groupid && x.LocationId == locationnid).OrderBy(x => x.jobtitlename), "Id", "jobtitlename", "Id");
            }
            else
            {
             result = new SelectList(unitOfWork.jobTitles.Get().OrderBy(x => x.jobtitlename), "Id", "jobtitlename", "Id");
            }

            return result;
        }

        public SelectList GetRole()
        {
            SelectList result = null;
            IList<IdentityRole> Role = new List<IdentityRole>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (HttpContext.Current.User.IsInRole("System Admin"))
                {
                    Role = db.Roles.Where(u => !u.Name.Contains("System Admin") && !u.Name.Contains("Head Of Unit") && !u.Name.Contains("Head HR") &&  !u.Name.Contains("Management") && !u.Name.Contains("Location Head") && !u.Name.Contains("Super Admin") && (!u.Name.Contains("L1") && !u.Name.Contains("L2") && !u.Name.Contains("L3"))).ToList();
                }
                else if (HttpContext.Current.User.IsInRole("HR"))
                {
                    Role = db.Roles.Where(u => !u.Name.Contains("System Admin") && !u.Name.Contains("Head Of Unit") && !u.Name.Contains("Head HR") && !u.Name.Contains("Management") && !u.Name.Contains("Location Head") && !u.Name.Contains("Super Admin") && !u.Name.Contains("Super Admin") && (!u.Name.Contains("L1") && !u.Name.Contains("L2") && !u.Name.Contains("L3"))).ToList();
                }
                else if (HttpContext.Current.User.IsInRole("Super Admin"))
                {
                    Role = db.Roles.Where(u => (!u.Name.Contains("L1") && !u.Name.Contains("L2") && !u.Name.Contains("L3") && !u.Name.Contains("Head Of Unit") && !u.Name.Contains("Super Admin"))).ToList();
                }
            }
            result = new SelectList(Role, "Id", "name", "Id");
            return result;
        }

        public SelectList GetEmploymentStatus()
        {
            var result = new SelectList(unitOfWork.employmentStatus.Get().Select(x => new { name = x.employemntStatus, id = x.empstId }), "id", "name", "id");
            return result;
        }
        public SelectList GetPrefix()
        {
            var result = new SelectList(unitOfWork.prefix.Get().Select(x => new { name = x.prefixName, Id = x.Id }), "Id", "name");
            return result;
        }
        public SelectList GetLevel()
        {
            var result = new SelectList(unitOfWork.Levels.Get().OrderBy(x => x.levelNo), "Id", "LevelNo", "Id");
            return result;
        }

        public SelectList GetDepartmentByUnit(int id)
        {
            var result = new SelectList(unitOfWork.Department.Get(filter: x => x.BusinessUnitsId == id).Select(x => new { Id = x.Id, name = x.deptname }), "Id", "name");
            return result;
        }

        public SelectList GetConsequence()
        {
            var result = new SelectList(unitOfWork.Consequence.Get().Select(x => new { name = x.Name, Id = x.ID }), "Id", "name");
            return result;
        }
        public SelectList GetViolation()
        {
            var result = new SelectList(unitOfWork.Violation.Get().Select(x => new { name = x.Name, Id = x.ID }), "Id", "name");
            return result;
        }

        public SelectList GetAllMonths()
        {
            var result = new SelectList(unitOfWork.GetDbContext().Month.ToList().Select(x => new { Id = x.id, name = x.MonthName }), "Id", "name");
            return result;
        }

        public List<MonthList> GetAllMonthList()
        {
            return unitOfWork.GetDbContext().MonthList.ToList();
        }
        public SelectList GetWeekDays()
        {
            var result = new SelectList(unitOfWork.GetDbContext().WeekDay.OrderByDescending(x => x.id), "id", "DayLongCode", "id");
            return result;
        }
        public List<Week> GetWeeks()
        {
            return unitOfWork.GetDbContext().Week.ToList();
        }
    }
}