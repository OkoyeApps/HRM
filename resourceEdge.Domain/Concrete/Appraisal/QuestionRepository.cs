using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class QuestionRepository : IQuestions
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> Get()
        {
            return unitOfWork.Questions.Get();
        }

        public IEnumerable<Question> GetAllQuestionsEagerly(string properties)=> unitOfWork.Questions.Get(includeProperties: properties);


        public IEnumerable<Question> GetAllUserQuestion(string userId)
        {
            var result = unitOfWork.Questions.Get(filter: x => x.UserIdForQuestion == userId);
            return result ?? null;
        }

        public Question GetById(int id)
        {
            return unitOfWork.Questions.GetByID(id);
        }

        public Question GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(Question entity)
        {
            unitOfWork.Questions.Insert(entity);
            unitOfWork.Save();
        }

        public void update(Question entity)
        {
            unitOfWork.Questions.Update(entity);
            unitOfWork.Save();
        }
    }
}
