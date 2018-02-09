using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class FileRepository : IFiles
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
  
        public IEnumerable<Files> Get()=> unitOfWork.Files.Get();
        public Files GetById(int id) => unitOfWork.Files.GetByID(id);
        public Files GetByUserId(string userId) => unitOfWork.GetDbContext().Files.Where(x => x.UserId == userId).FirstOrDefault();
        public void Insert(Files entity)
        {
            unitOfWork.Files.Insert(entity);
            unitOfWork.Save();
        }
        public void update(Files entity)
        {
            unitOfWork.Files.Update(entity);
            unitOfWork.Save();
        }
        public void Delete(int id)
        {
            unitOfWork.Files.Delete(id);
            unitOfWork.Save();
        }
       


        
    }
}
