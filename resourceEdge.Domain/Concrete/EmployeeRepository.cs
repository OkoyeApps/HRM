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

        public void Insert(Employee employees)
        {
            unitOfWork.employees.Insert(employees);
            unitOfWork.Save();
        }
        public IEnumerable<Employee> Get() => unitOfWork.employees.Get();
        public Employee GetById(int id) => unitOfWork.employees.GetByID(id);
        public void Delete(int id)
        {
            unitOfWork.employees.Delete(id);
            unitOfWork.Save();
        }
        public void update(Employee employee)
        {
            unitOfWork.employees.Update(employee);
            unitOfWork.Save();
        }
        public Employee GetEmployeeByEmail(string email)
        {
            var emp = unitOfWork.GetDbContext().Employee.Where(x => x.empEmail == email);
            var employee = unitOfWork.GetDbContext().Employee.Where(x => x.empEmail == email).FirstOrDefault();
            return employee;
        }

        public Employee GetByUserId(string userId)
        {
           var result =  unitOfWork.employees.Get(filter: x => x.userId == userId).FirstOrDefault();
            return result ?? null;
        }

       public List<Employee> GetEmpByBusinessUnit(int id)
        {
            var result = unitOfWork.employees.Get(filter: x => x.businessunitId == id).ToList();
            return result ?? null;
        }

        public Employee CheckIfEmployeeExistByUserId(string userId)
        {
            var result = unitOfWork.employees.Get(filter: x => x.userId == userId).FirstOrDefault();
            return result ?? null;
        }
        public List<Employee> GetEmployeeByDepts(int dept)
        {
            var result = unitOfWork.employees.Get(filter: x => x.DepartmentId == dept).ToList();
            return result ?? null;
        }

        public List<Employee> GetUnitHead(int unitId)
        {
            var result = unitOfWork.employees.Get(filter: x => x.businessunitId == unitId && x.IsUnithead == true).ToList();
            return result ?? null;
        }

        public List<Employee> GetHrs()
        {
            var result = unitOfWork.employees.Get(filter: x => x.empRoleId == 3).ToList();
            return result ?? null;
        }
        public List<Employee> GetEmployeeUnitMembers(int unitId)
        {
            var result = unitOfWork.employees.Get(x => x.businessunitId == unitId && x.IsUnithead != true).ToList();
            return result ?? null;
        }

        public List<Employee> GetReportManagers(int unitId)
        {
            var result = unitOfWork.employees.Get(filter: x =>x.businessunitId == unitId && x.empRoleId == 2).ToList();
            return result ?? null;
        }

        public List<Employee> GetAllHrsBYGroup(int groupId)
        {
            var result = unitOfWork.employees.Get(filter: x => x.GroupId == groupId).ToList();
            return result ?? null;
        }

        public Employee GetEmployeeByGroupId(string userId, int groupId)
        {
            var result = unitOfWork.employees.Get(filter: x => x.userId == userId && x.GroupId == groupId).FirstOrDefault();
            return result ?? null;
        }

        public IEnumerable<Employee> GetAllEmployeesByGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeLazily(string userId)
        {
            var result = unitOfWork.employees.Get(filter: x=>x.userId == userId, includeProperties: "Location,Deparments, Levels,Groups").FirstOrDefault();
            return result;
        }

        public IEnumerable<Employee> GetAllEmployeesByLocation(int id) => unitOfWork.employees.Get(filter: x => x.LocationId == id);
    }
}
