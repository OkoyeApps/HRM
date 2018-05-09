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
             result = new SelectList(unitOfWork.BusinessUnit.Get().OrderBy(x => x.unitname), "Id", "unitname");
            return result;
        }
       

        public SelectList GetJobtitle()
        {
            var result = new SelectList(unitOfWork.jobTitles.Get().OrderBy(x => x.jobtitlename), "Id", "jobtitlename", "Id");
            return result;
        }

        public SelectList GetRole()
        {
            SelectList result = null;
            IList<IdentityRole> Role = new List<IdentityRole>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var allRoles = db.Roles.Where(u => !u.Name.Contains("System Admin") && !u.Name.Contains("HR") && !u.Name.Contains("Head HR") && !u.Name.Contains("Head Of Unit") && !u.Name.Contains("Management") && !u.Name.Contains("Location Head") && !u.Name.Contains("Management") && (!u.Name.Contains("L1") && !u.Name.Contains("L2") && !u.Name.Contains("L3"))).ToList();
                allRoles.ForEach(x => Role.Add(x));
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