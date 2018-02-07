using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface GenericInterface<IEntity> where IEntity : class
    {
        
        IEnumerable<IEntity> Get();
        IEntity GetById(int id);
        void Insert(IEntity entity);
        void Delete(int id);
        void update(IEntity entity);
    }
}
