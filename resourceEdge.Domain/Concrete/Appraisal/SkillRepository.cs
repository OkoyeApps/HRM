using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Concrete
{
    public class SkillRepository : ISkills
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Skills> Get()
        {
            return unitOfWork.Skills.Get();
        }

        public Skills GetById(int id)
        {
            return unitOfWork.Skills.GetByID(id);
        }

        public Skills GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Skills entity)
        {
            unitOfWork.Skills.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Skills entity)
        {
            unitOfWork.Skills.Update(entity);
            unitOfWork.Save();
        }
    }
}
