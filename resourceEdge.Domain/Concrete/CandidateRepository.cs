using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;

namespace resourceEdge.Domain.Concrete
{
    public class CandidateRepository : ICandidate
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Candidate> Get()
        {
            return unitOfWork.Candidate.Get();
        }

        public Candidate GetById(int id)
        {
            return unitOfWork.Candidate.GetByID(id);
        }

        public Candidate GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Candidate entity)
        {
            unitOfWork.Candidate.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Candidate entity)
        {
            unitOfWork.Candidate.Update(entity);
            unitOfWork.Save();
        }
    }
}
