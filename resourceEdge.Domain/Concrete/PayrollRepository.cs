using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    class PayrollRepository : IPayroll
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payroll> Get()
        {
           return unitOfWork.PayRoll.Get();
        }

        public Payroll GetById(int id)
        {
           return unitOfWork.PayRoll.GetByID(id);
        }

        public void Insert(Payroll entity)
        {
            unitOfWork.PayRoll.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Payroll entity)
        {
            unitOfWork.PayRoll.Update(entity);
            unitOfWork.Save();
        }
    }
}
