using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IBusinessUnits : GenericInterface<BusinessUnit>
    {
       BusinessUnit DoesUnitExitByName(string Name);
       BusinessUnit GetUnitByLocation(int locationId, string unitName);
       List<BusinessUnit> GetUnitsByLocation(int locationId);
    }
}
