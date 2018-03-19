using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class JobTitleRepository : IJobtitles
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Insert(Jobtitle jobs)
        {
            unitOfWork.jobTitles.Insert(jobs);
            unitOfWork.Save();
        }
        public void Delete(int id)
        {
            unitOfWork.jobTitles.Delete(id);
        }
        public IEnumerable<Jobtitle> Get() => unitOfWork.jobTitles.Get();
        public Jobtitle GetById(int id) => unitOfWork.jobTitles.GetByID(id);

        public void update(Jobtitle entity)
        {
            throw new NotImplementedException();
        }

        public Jobtitle GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
