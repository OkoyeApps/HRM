using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class AppraisalQuestionRepository : IAppraisalQuestion
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppraisalQuestion> Get() => unitOfWork.AppraisalQuestion.Get();

        public AppraisalQuestion GetById(int id) => unitOfWork.AppraisalQuestion.GetByID(id);

        public AppraisalQuestion GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(AppraisalQuestion entity)
        {
            unitOfWork.AppraisalQuestion.Insert(entity);
            unitOfWork.Save();
        }

        public void update(AppraisalQuestion entity)
        {
            unitOfWork.AppraisalQuestion.Update(entity);
            unitOfWork.Save();
        }
    }
}
