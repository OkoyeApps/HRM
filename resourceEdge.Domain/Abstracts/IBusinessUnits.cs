using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IBusinessUnits : GenericInterface<BusinessUnits>
    {
       BusinessUnits DoesUnitExitByName(string Name);
       BusinessUnits GetUnitByLocation(int locationId, string unitName);
       List<BusinessUnits> GetUnitsByLocation(int locationId);
    }
}
