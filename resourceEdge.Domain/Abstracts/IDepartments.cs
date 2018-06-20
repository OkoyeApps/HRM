using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
   public interface IDepartments
    {
        IEnumerable<Department> Getdepartment();
        Department GetdepartmentById(int id);
        void addepartment(Department department);
        void Updatedepartment(Department department);
        void DeleteDepartment(int id);
        List<Department> GetDepartmentByUnit(int id);
    }
}
