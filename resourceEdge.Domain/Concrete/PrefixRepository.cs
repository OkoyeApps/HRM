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
        public void addprefixes(Prefixes prefix)
        {
            unitofWork.prefix.Insert(prefix);
            unitofWork.Save();
        }
        public void DeletePrefixes(int id)
        {
            unitofWork.prefix.Delete(id);
            unitofWork.Save();
        }
        public IEnumerable<Prefixes> GetPrefixes() => unitofWork.prefix.Get();
        public Prefixes GetPrefixesById(int id) => unitofWork.prefix.GetByID(id);
    }
}
