using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
 public   class LevelRepository : ILevels
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Levels> Get()=> unitOfWork.Levels.Get();
        

        public Levels GetById(int id)=> unitOfWork.Levels.GetByID(id);

        public Levels GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Levels entity)
        {
            unitOfWork.Levels.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Levels entity)
        {
            unitOfWork.Levels.Update(entity);
            unitOfWork.Save();
        }
    }
}
