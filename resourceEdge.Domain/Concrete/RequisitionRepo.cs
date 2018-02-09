using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class RequisitionRepo : IRequisition
    {
        UnitofWork.UnitOfWork unitofWork  = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            unitofWork.Requisition.Delete(id);
            unitofWork.Save();
        }

        public IEnumerable<Requisition> Get()
        {
            return unitofWork.Requisition.Get();
        }

        public Requisition GetById(int id)
        {
            return unitofWork.Requisition.GetByID(id);
        }

        public Requisition GetByUserId(string userId)
        {
            return unitofWork.GetDbContext().Requisition.Where(x => x.ReportingId == userId).FirstOrDefault();
        }

        public void Insert(Requisition entity)
        {
            unitofWork.Requisition.Insert(entity);
            unitofWork.Save();
        }

        public void update(Requisition entity)
        {
            unitofWork.Requisition.Update(entity);
            unitofWork.Save();
        }
    }
}
