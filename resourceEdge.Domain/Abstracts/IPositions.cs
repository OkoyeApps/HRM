using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
   public  interface IPositions
    {
        void AddPosition(Positions pposition);
        IEnumerable<Positions> GetPosition();
        Positions GetPositionById(int id);
        void DeletePosition(int id);
    }
}
