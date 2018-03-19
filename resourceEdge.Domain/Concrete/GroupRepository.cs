using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
   public class GroupRepository : IGroups
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> Get() => unitOfWork.Groups.Get();

        public Group GetById(int id) => unitOfWork.Groups.GetByID(id);

        public Group GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Group entity)
        {
            unitOfWork.Groups.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Group entity)
        {
            unitOfWork.Groups.Update(entity);
            unitOfWork.Save();
        }
    }
}
