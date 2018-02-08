using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IEmployees
    {
        IEnumerable<Employees> getEmployees();
        Employees getEmployeesById(int? id);
        void addEmployees(Employees employees);
        void UpdateEmployees(Employees employee);
        void RemoveEmployees(int? id);
        int GetEmployeeByEmail(string email);
        Employees GetEmployeeByUserid(string userid);
    }
}
