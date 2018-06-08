
using Microsoft.AspNet.Identity;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.Infrastructures;
using resourceEdge.Domain.UnitofWork;
using resourceEdge.webUi.Models;
using resourceEdge.webUi.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace resourceEdge.webUi.Infrastructure.SystemManagers
{
    public class DisciplinaryManager
    {
        UnitOfWork unitofWork = new UnitOfWork();

        public string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }
        public Violation GetViolationById(int id)
        {
            return unitofWork.Violation.GetByID(id);
        }
        public Consequence GetConsequenceById(int id)
        {
            return unitofWork.Consequence.GetByID(id);
        }
        public DisciplineListItem GetIncidentById(int id)
        {
            var allIncident = unitofWork.Discipline.Get(filter: x => x.Id == id, includeProperties: "CorrectiveAction,Violation,Department,BusinessUnit")
           .Select(x => new DisciplineListItem
           {
               ID = x.Id,
               CorrectiveAction = x.CorrectiveAction.Name,
               violation = x.Violation.Name,
               Unit = x.BusinessUnit.unitname,
               Department = x.Department.deptname,
               FullName = x.EmployeeName,
               ReportManager = x.ReportingManager,
               Date = x.ExpiryDate.Date.ToShortDateString(),
               Response = x.EmployeeStatement,
               ExpiryDate = x.ExpiryDate
           }).FirstOrDefault();
            
            allIncident.FullName = unitofWork.employees.Get(filter: y => y.userId == allIncident.FullName).FirstOrDefault().FullName;
            allIncident.ReportManager = unitofWork.employees.Get(filter: y => y.userId == allIncident.ReportManager).FirstOrDefault().FullName;
             return allIncident;
        }
        public IEnumerable<Violation> AllViolation()
        {
            return unitofWork.Violation.Get();
        }
        public IEnumerable<Consequence> AllConsequence()
        {
            return unitofWork.Consequence.Get();
        }

        public bool AddOrUpdateViolation(FormCollection collection, Violation model = null, int? id = null)
        {
            if (id == null)
            {
                IDictionary<string, object> MyDictionary = new Dictionary<string, object>();
                collection.CopyTo(MyDictionary);

                var violations = MyDictionary.Keys.Where(X => X.StartsWith("name")).ToList();
                var Description = MyDictionary.Keys.Where(x => x.StartsWith("description")).ToList();

                var Maxlength = (MyDictionary.Count - 1) / 2;
                for (int i = 0; i < Maxlength; i++)
                {
                    if (MyDictionary.ContainsKey(violations[i]))
                    {
                        var Violation = new Violation
                        {
                            CreatedBy = GetUserId(),
                            CreatedOn = DateTime.Now,
                            IsActive = true,
                            Name = MyDictionary[violations[i]].ToString()
                        };
                        if (MyDictionary.ContainsKey(Description[i]))
                        {
                            Violation.Description = MyDictionary[Description[i]].ToString();
                        }
                        unitofWork.Violation.Insert(Violation);
                    }
                }
                unitofWork.Save();
                return true;
            }
            else
            {
                var currentViolation = unitofWork.Violation.GetByID(model.ID);
                if (currentViolation != null)
                {
                    currentViolation.Name = model.Name;
                    currentViolation.ModifiedBy = GetUserId();
                    currentViolation.ModifiedOn = DateTime.Now;
                    unitofWork.Violation.Update(currentViolation);
                    unitofWork.Save();
                    return true;
                }
            }
            return false;
        }

        public bool AddOrUpdateConsequences(FormCollection collection, Consequence model = null, int? id = null)
        {
            if (id == null)
            {
                IDictionary<string, object> MyDictionary = new Dictionary<string, object>();
                collection.CopyTo(MyDictionary);

                var name = MyDictionary.Keys.Where(X => X.StartsWith("name")).ToList();
                var Description = MyDictionary.Keys.Where(x => x.StartsWith("description")).ToList();

                var Maxlength = MyDictionary.Count / 2;
                for (int i = 0; i < Maxlength; i++)
                {
                    if (MyDictionary.ContainsKey(name[i]))
                    {
                        var Violation = new Consequence
                        {
                            CreatedBy = GetUserId(),
                            CreatedOn = DateTime.Now,
                            IsActive = true,
                            Name = MyDictionary[name[i]].ToString()
                        };
                        if (MyDictionary.ContainsKey(Description[i]))
                        {
                            Violation.Description = MyDictionary[Description[i]].ToString();
                        }
                        unitofWork.Consequence.Insert(Violation);
                    }
                }
                unitofWork.Save();
                return true;
            }
            else
            {
                var currentConsequence = unitofWork.Consequence.GetByID(model.ID);
                if (currentConsequence != null)
                {
                    currentConsequence.Name = model.Name;
                    currentConsequence.Description = model.Description;
                    currentConsequence.ModifiedBy = GetUserId();
                    currentConsequence.ModifiedOn = DateTime.Now;
                    unitofWork.Consequence.Update(currentConsequence);
                    unitofWork.Save();
                    return true;
                }
            }
            return false;
        }

        public bool AddOrUpdateDisciplineIncident(DisciplinaryIncidentViewModel model, DisciplineListItem EditModel, int? Id = null)
        {
            if (Id == null)
            {
                if (model.BusinessUnit > 0 && model.CorrectiveAction > 0 && model.ViolationId > 0)
                {
                    var userDetails = (SessionModel)HttpContext.Current.Session["_ResourceEdgeTeneceIdentity"];
                    var incident = new DisciplinaryIncident
                    {
                        BusinessUnitId = model.BusinessUnit,
                        CorrectiveActionId = model.CorrectiveAction,
                        CreatedBy = GetUserId(),
                        CreatedOn = DateTime.Now,
                        DateOfOccurence = model.DateOfOccurence,
                        ExpiryDate = model.ExpiryDate,
                        JobTitle = model.JobTitle,
                        ReportingManager = model.ReportingManager,
                        ViolationId = model.ViolationId,
                        ViolationDescription = model.ViolationDescription,
                        Verdict = (int)model.Verdict,
                        RaisedBy = GetUserId(),
                        EmployeeName = model.EmployeeName,
                        DepartmentId = model.Department,
                        EmployeeAppeal = (int)model.EmployeeAppeal,
                        IsActive = true, 
                         GroupId = userDetails.GroupId.Value,
                          LocationId = userDetails.LocationId.Value
                    };
                    unitofWork.Discipline.Insert(incident);
                    unitofWork.Save();
                    return true;
                }
            }
            else
            {
                var currentIncident = unitofWork.Discipline.GetByID(Id);
                if (currentIncident != null)
                {
                    //currentIncident.EmployeeAppeal = (int)model.EmployeeAppeal;
                    currentIncident.EmployeeStatement = EditModel.Response;
                    currentIncident.ModifiedBy = GetUserId();
                    currentIncident.ModifiedOn = DateTime.Now;
                    unitofWork.Discipline.Update(currentIncident);
                    unitofWork.Save();
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<DisciplineListItem> AllIncident(int groupId, int LocationId)
        {
            var allIncident = unitofWork.Discipline.Get(filter: x => x.GroupId == groupId && x.LocationId == LocationId, includeProperties: "CorrectiveAction,Violation,Department,BusinessUnit")
                .Select(x => new DisciplineListItem
                {
                    ID = x.Id,
                    CorrectiveAction = x.CorrectiveAction.Name,
                    violation = x.Violation.Name,
                    Unit = x.BusinessUnit.unitname,
                    Department = x.Department.deptname,
                    FullName = x.EmployeeName,
                    ReportManager = x.ReportingManager,
                    Date = x.ExpiryDate.Date.ToShortDateString(),
                }).ToList();
            allIncident.ToList().ForEach(x => x.FullName = unitofWork.employees.Get(filter: y => y.userId == x.FullName).FirstOrDefault().FullName);
            allIncident.ToList().ForEach(x => x.ReportManager = unitofWork.employees.Get(filter: y => y.userId == x.ReportManager).FirstOrDefault().FullName);
            return allIncident;
        }

        public DisciplineListItem IncidentDetail(int groupId, int LocationId, int Id)
        {
            try
            {
                var allIncident = unitofWork.Discipline.Get(filter: x => x.GroupId == groupId && x.LocationId == LocationId, includeProperties: "CorrectiveAction,Violation,Department,BusinessUnit")
                 .Select(x => new DisciplineListItem
                 {
                     ID = x.Id,
                     CorrectiveAction = x.CorrectiveAction.Name,
                     violation = x.Violation.Name,
                     Unit = x.BusinessUnit.unitname,
                     Department = x.Department.deptname,
                     FullName = x.EmployeeName,
                     ReportManager = x.ReportingManager,
                     Date = x.ExpiryDate.Date.ToShortDateString(),
                     Response = x.EmployeeStatement
                 }).FirstOrDefault();
                allIncident.FullName = unitofWork.employees.Get(filter: y => y.userId == allIncident.FullName).FirstOrDefault().FullName;
                allIncident.ReportManager = unitofWork.employees.Get(filter: y => y.userId == allIncident.ReportManager).FirstOrDefault().FullName;
                return allIncident;
            }
            catch
            {
                throw new Exception("No incidents found, please try loging in again if problem persist");
            }
        }

        public IEnumerable<DisciplineListItem> MyIncident()
        {
            var user = GetUserId();
            var allIncident = unitofWork.Discipline.Get(filter: x => x.EmployeeName == user ,includeProperties: "CorrectiveAction,Violation,Department,BusinessUnit")
                .Select(x => new DisciplineListItem
                {
                    ID = x.Id,
                    CorrectiveAction = x.CorrectiveAction.Name,
                    violation = x.Violation.Name,
                    Unit = x.BusinessUnit.unitname,
                    Department = x.Department.deptname,
                    FullName = x.EmployeeName,
                    ReportManager = x.ReportingManager,
                    Date = x.ExpiryDate.Date.ToShortDateString(),
                    Status = x.IsActive,
                    Response = x.EmployeeStatement,
                    Appeal = x.EmployeeAppeal,
                    ExpiryDate = x.ExpiryDate
                }).ToList();
            allIncident.ToList().ForEach(x => x.FullName = unitofWork.employees.Get(filter: y => y.userId == x.FullName).FirstOrDefault().FullName);
            allIncident.ToList().ForEach(x => x.ReportManager = unitofWork.employees.Get(filter: y => y.userId == x.ReportManager).FirstOrDefault().FullName);
            return allIncident;
        }
        public bool DeleteIncident(int id)
        {
            var result = unitofWork.Discipline.GetByID(id);
            if (result != null)
            {
                unitofWork.Discipline.Delete(result);
                unitofWork.Save();
                return true;
            }
            return false;
        }
    }
}