using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class ParameterRepository : IParameter
    {
        UnitofWork.UnitOfWork unitofWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parameters> Get() => unitofWork.Parameters.Get();

        public Parameters GetById(int id) => unitofWork.Parameters.GetByID(id);

        public Parameters GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Parameters entity)
        {
            unitofWork.Parameters.Insert(entity);
            unitofWork.Save();
        }

        public void update(Parameters entity)
        {
            unitofWork.Parameters.Update(entity);
            unitofWork.Save();
        }
    }
}
