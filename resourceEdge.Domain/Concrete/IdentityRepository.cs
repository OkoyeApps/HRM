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
        public void addIdentityCode(IdentityCodes codes)
        {
            unitofwork.identityCodes.Insert(codes);
            unitofwork.Save();
        }
        public IdentityCodes GetIdentityById(int id) => unitofwork.identityCodes.GetByID(id);
        public IEnumerable<IdentityCodes> GetIdentityCodes() => unitofwork.identityCodes.Get();

        public void removeIdentityCode(IdentityCodes codes)
        {
            unitofwork.identityCodes.Delete(codes);
            unitofwork.Save();
        }
        public void updateIdentityCode(IdentityCodes code)
        {
            unitofwork.identityCodes.Update(code);
            unitofwork.Save();
        }
    }
}
