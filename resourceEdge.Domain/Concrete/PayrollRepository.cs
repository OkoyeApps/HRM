using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;
using System.Data.Entity.Migrations;

namespace resourceEdge.Domain.Concrete
{
   public class PayrollRepository : IPayroll
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmpPayroll> Get()
        {
           return unitOfWork.PayRoll.Get();
        }

        public EmpPayroll GetById(int id)
        {
           return unitOfWork.PayRoll.GetByID(id);
        }

        public EmpPayroll GetByUserId(string UserId)
        {
            var employees = Get().ToList();
            var currentEmployee = employees.Find(x => x.UserId == UserId);
            return currentEmployee;
        }

        public void Insert(EmpPayroll entity)
        {
            unitOfWork.PayRoll.Insert(entity);
            unitOfWork.Save();
        }

        public void update(EmpPayroll entity)
        {
            unitOfWork.PayRoll.Update(entity);
            unitOfWork.Save();
        }

       
    }
}
