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

        public void addEmployees(Employees employees)
        {
            unitOfWork.employees.Insert(employees);
            unitOfWork.Save();
        }
        public IEnumerable<Employees> getEmployees() => unitOfWork.employees.Get();
        public Employees getEmployeesById(int? id) => unitOfWork.employees.GetByID(id);
        public void RemoveEmployees(int? id)
        {
            unitOfWork.employees.Delete(id);
            unitOfWork.Save();
        }
        public void UpdateEmployees(Employees employee)
        {
            unitOfWork.employees.Update(employee);
            unitOfWork.Save();
        }
        public int GetEmployeeByEmail(string email)
        {
            var emp = unitOfWork.GetDbContext().employees.Where(x => x.empEmail == email);
            var employee = unitOfWork.GetDbContext().employees.Find(email);
            return employee.empID;
        }

        public Employees GetEmployeeByUserid(string userId)
        {
            List<Employees> employee = getEmployees().ToList();
            var result = employee.Find(x => x.userId == userId);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
