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
            unitOfWork.Levels.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<Level> Get()=> unitOfWork.Levels.Get();
        

        public Level GetById(int id)=> unitOfWork.Levels.GetByID(id);

        public Level GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Level entity)
        {
            unitOfWork.Levels.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Level entity)
        {
            unitOfWork.Levels.Update(entity);
            unitOfWork.Save();
        }
    }
}
