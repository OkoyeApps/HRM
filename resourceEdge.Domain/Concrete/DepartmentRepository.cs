using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class DepartmentRepository : IDepartments
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void addepartment(Department department)
        {
            unitOfWork.Department.Insert(department);
            unitOfWork.Save();
        }
        public IEnumerable<Department> Getdepartment() => unitOfWork.Department.Get();
        public Department GetdepartmentById(int id) => unitOfWork.Department.GetByID(id);
        public void DeleteDepartment(int id)
        {
            unitOfWork.Department.Delete(id);
            unitOfWork.Save();
        }
        public void Updatedepartment(Department department)
        {
            unitOfWork.Department.Update(department);
            unitOfWork.Save();
        }

        public List<Department> GetDepartmentByUnit(int id)
        {
            var result = unitOfWork.Department.Get(filter: x => x.BusinessUnitsId == id).ToList();
            return result ?? null;
        }
    }
}
