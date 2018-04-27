using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IEmployees : GenericInterface<Employee>
    {

        Employee GetEmployeeLazily(string userId);
        Employee GetEmployeeByEmail(string email);
        List<Employee> GetEmpByBusinessUnit(int id);
        IEnumerable<Employee> GetAllEmployeesByLocation(int id);
        Employee CheckIfEmployeeExistByUserId(string userId);
        List<Employee> GetEmployeeByDepts(int dept);
        List<Employee> GetUnitHead(int unitId);
        List<Employee> GetHrs();
        List<Employee> GetEmployeeUnitMembers(int unitId);
        List<Employee> GetReportManagers(int unitId);
        List<Employee> GetAllHrsBYGroup(int groupId);
        Employee GetEmployeeByGroupId(string userId, int groupId);
        IEnumerable<Employee> GetAllEmployeesByGroup(int groupId);

    }
}
