using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class PrefixRepository : IPrefixes
    {
        UnitofWork.UnitOfWork unitofWork = new UnitofWork.UnitOfWork();
        public void Insert(Prefix prefix)
        {
            unitofWork.prefix.Insert(prefix);
            unitofWork.Save();
        }
        public void Delete(int id)
        {
            unitofWork.prefix.Delete(id);
            unitofWork.Save();
        }
        public IEnumerable<Prefix> Get() => unitofWork.prefix.Get();
        public Prefix GetById(int id) => unitofWork.prefix.GetByID(id);

        public void update(Prefix entity)
        {
            unitofWork.prefix.Update(entity);
            unitofWork.Save();
        }

        public Prefix GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
