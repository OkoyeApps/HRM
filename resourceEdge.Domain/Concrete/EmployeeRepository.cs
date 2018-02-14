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
            List<Employees> employee = Get().ToList();
            var result = employee.Find(x => x.userId == userId);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
