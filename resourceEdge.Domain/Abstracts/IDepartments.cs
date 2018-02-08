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
        IEnumerable<Departments> Getdepartment();
        Departments GetdepartmentById(int id);
        void addepartment(Departments department);
        void Updatedepartment(Departments department);
        void DeleteDepartment(int id);
    }
}
