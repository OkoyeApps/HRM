using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;

namespace resourceEdge.Domain.Concrete
{
    public class EmployeeRepository : IEmployees
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public void Insert(Employees employees)
        {
            unitOfWork.employees.Insert(employees);
            unitOfWork.Save();
        }
        public IEnumerable<Employees> Get() => unitOfWork.employees.Get();
        public Employees GetById(int id) => unitOfWork.employees.GetByID(id);
        public void Delete(int id)
        {
            unitOfWork.employees.Delete(id);
            unitOfWork.Save();
        }
        public void update(Employees employee)
        {
            unitOfWork.employees.Update(employee);
            unitOfWork.Save();
        }
        public Employees GetEmployeeByEmail(string email)
        {
            var emp = unitOfWork.GetDbContext().employees.Where(x => x.empEmail == email);
            var employee = unitOfWork.GetDbContext().employees.Where(x => x.empEmail == email).FirstOrDefault();
            return employee;
        }

        public Employees GetByUserId(string userId)
        {
            //List<Employees> employee = Get().ToList();
            //var result = employee.Find(x => x.userId == userId);
           var result =  unitOfWork.employees.Get(filter: x => x.userId == userId).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            return null;
        }

       public List<Employees> GetEmpByBusinessUnit(int id)
        {
            var result = unitOfWork.employees.Get(filter: x => x.businessunitId == id).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public Employees CheckIfEmployeeExistByUserId(string userId)
        {
            var result = unitOfWork.employees.Get(filter: x => x.userId == userId).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<Employees> GetEmployeeByDepts(int dept)
        {
            var result = unitOfWork.employees.Get(filter: x => x.departmentId == dept).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<Employees> GetUnitHead(int unitId)
        {
            var result = unitOfWork.employees.Get(filter: x => x.businessunitId == unitId && x.IsUnithead == true).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<Employees> GetHrs()
        {
            var result = unitOfWork.employees.Get(filter: x => x.empRoleId == 3).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
        public List<Employees> GetEmployeeUnitMembers(int unitId)
        {
            var result = unitOfWork.employees.Get(x => x.businessunitId == unitId && x.IsUnithead != true).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public List<Employees> GetReportManagers(string userId, int unitId)
        {
            var result = unitOfWork.employees.Get(filter: x =>x.businessunitId == unitId && x.empID == 2).ToList();
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
