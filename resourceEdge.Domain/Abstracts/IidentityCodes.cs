using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
   public  interface IidentityCodes
    {
        IEnumerable<IdentityCodes> GetIdentityCodes();
        IdentityCodes GetIdentityById(int id);
        void addIdentityCode(IdentityCodes codes);
        void removeIdentityCode(IdentityCodes codes);
        void updateIdentityCode(IdentityCodes code);
    }
}
