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
  
        public IEnumerable<File> Get()=> unitOfWork.Files.Get();
        public File GetById(int id) => unitOfWork.Files.GetByID(id);
        public File GetByUserId(string userId) => unitOfWork.GetDbContext().File.Where(x => x.UserId == userId).FirstOrDefault();
        public void Insert(File entity)
        {
            unitOfWork.Files.Insert(entity);
            unitOfWork.Save();
        }
        public void update(File entity)
        {
            unitOfWork.Files.Update(entity);
            unitOfWork.Save();
        }
        public void Delete(int id)
        {
            unitOfWork.Files.Delete(id);
            unitOfWork.Save();
        }

        public File GetFileByUserId(string userId, FileType type)
        {
            var result = unitOfWork.Files.Get(filter: x => x.UserId == userId && x.FileType == FileType.Avatar).FirstOrDefault();
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
