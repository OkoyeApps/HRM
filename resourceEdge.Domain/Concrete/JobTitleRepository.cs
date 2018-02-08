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
        public void addJobTitles(Jobtitles jobs)
        {
            unitOfWork.jobTitles.Insert(jobs);
            unitOfWork.Save();
        }
        public void deleteJobTitles(int id)
        {
            unitOfWork.jobTitles.Delete(id);
        }
        public IEnumerable<Jobtitles> GetJobTitles() => unitOfWork.jobTitles.Get();
        public Jobtitles GetJobTitlesById(int id) => unitOfWork.jobTitles.GetByID(id);
    }
}
