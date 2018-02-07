using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface GenericAbstract<TEntity> where TEntity : class
    {
          Task<IEnumerable<TEntity>> GetAll();
          void Add(TEntity entity);
          Task<TEntity> GetById(int? id);
          void Remove(TEntity entity);
          void Remove(int? id);
    }
}
