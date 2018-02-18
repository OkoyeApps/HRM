using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IEmployees : GenericInterface<Employees>
    {
        Employees GetEmployeeByEmail(string email);
        List<Employees> GetEmpByBusinessUnit(int id);
        Employees CheckIfEmployeeExistByUserId(string userId);
        List<Employees> GetEmployeeByDepts(int dept);
        List<Employees> GetUnitHead(int unitId);
        List<Employees> GetHrs();
        List<Employees> GetEmployeeUnitMembers(int unitId);
        List<Employees> GetReportManagers(string userId, int unitId);

    }
}
