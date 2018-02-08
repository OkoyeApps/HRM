using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class PositionRepository : IPositions
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void AddPosition(Positions position)
        {
            unitOfWork.positions.Insert(position);
            unitOfWork.Save();
        }
        public void DeletePosition(int id)
        {
            unitOfWork.positions.Delete(id);
            unitOfWork.Save();
        }
        public IEnumerable<Positions> GetPosition() => unitOfWork.positions.Get();
        public Positions GetPositionById(int id) => unitOfWork.positions.GetByID(id);

        //public IEnumerable<Positions> GetPositionAndJobs()
        //{
        //    var position = unitOfWork.positions.Get();
            
        //    //List<Jobtitles> jobs = new List<Jobtitles>();
        //    //foreach (var item in position)
        //    //{
        //    //    var jobTitle = item.Jobtitle;
        //    //    jobs.Add(jobTitle);
        //    //}

        //}

    }
}
