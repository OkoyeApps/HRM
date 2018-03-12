using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    class EmployeeDetailDispatchRepository : IEmployeeDetailDispatcher
    {
        UnitofWork.UnitOfWork unitOfWork = new Domain.UnitofWork.UnitOfWork();

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDetailDispatcher> Get() => unitOfWork.EmpDetailDispatch.Get();

        public EmployeeDetailDispatcher GetById(int id) => unitOfWork.EmpDetailDispatch.GetByID(id);


        public EmployeeDetailDispatcher GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(EmployeeDetailDispatcher entity)
        {
            unitOfWork.EmpDetailDispatch.Insert(entity);
            unitOfWork.Save();
        }

        public void update(EmployeeDetailDispatcher entity)
        {
            unitOfWork.EmpDetailDispatch.Update(entity);
            unitOfWork.Save();
        }
    }
}
