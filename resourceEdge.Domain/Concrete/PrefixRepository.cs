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
        public void Insert(Prefixes prefix)
        {
            unitofWork.prefix.Insert(prefix);
            unitofWork.Save();
        }
        public void Delete(int id)
        {
            unitofWork.prefix.Delete(id);
            unitofWork.Save();
        }
        public IEnumerable<Prefixes> Get() => unitofWork.prefix.Get();
        public Prefixes GetById(int id) => unitofWork.prefix.GetByID(id);

        public void update(Prefixes entity)
        {
            throw new NotImplementedException();
        }

        public Prefixes GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
