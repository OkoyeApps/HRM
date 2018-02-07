using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IBusinessUnits
    {
        IEnumerable<BusinessUnits> GetBusinessUnit();
        BusinessUnits GetBusinessUnitById(int? id);
        void addbusinessunit(BusinessUnits businessunit);
        void UpdateBusinessunit(BusinessUnits businessunit);
        void RemoveBusinessunit(int id);
    }
}
