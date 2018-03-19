using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
   public class IdentityRepository : IidentityCodes
    {
        UnitOfWork unitofwork = new UnitOfWork();
        public void Insert(IdentityCode codes)
        {
            unitofwork.identityCodes.Insert(codes);
            unitofwork.Save();
        }
        public IdentityCode GetById(int id) => unitofwork.identityCodes.GetByID(id);
        public IEnumerable<IdentityCode> Get() => unitofwork.identityCodes.Get();

        public void Delete(int id)
        {
            unitofwork.identityCodes.Delete(id);
            unitofwork.Save();
        }
        public void update(IdentityCode code)
        {
            unitofwork.identityCodes.Update(code);
            unitofwork.Save();
        }

        public IdentityCode GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
