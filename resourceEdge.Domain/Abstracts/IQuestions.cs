using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IQuestions : GenericInterface<Question>
    {
        IEnumerable<Question> GetAllUserQuestion(string userId);
        IEnumerable<Question> GetAllQuestionsEagerly(string properties);
    }
}
