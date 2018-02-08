using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IPayroll : GenericInterface<EmpPayroll>
    {
        EmpPayroll GetByUserId(string UserId);
        void AddORUpdate(string userId, EmpPayroll entity);
    }
}
