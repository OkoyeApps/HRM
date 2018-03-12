using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class MailDispatcherRepository : IMailDispatcher
    {
        UnitofWork.UnitOfWork unitIfWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MailDispatcher> Get() => unitIfWork.MailDispatch.Get();

        public MailDispatcher GetById(int id) => unitIfWork.MailDispatch.GetByID(id);

        public MailDispatcher GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void Insert(MailDispatcher entity)
        {
            unitIfWork.MailDispatch.Insert(entity);
            unitIfWork.Save();
        }

        public void update(MailDispatcher entity)
        {
            unitIfWork.MailDispatch.Update(entity);
            unitIfWork.Save();
        }
    }
}
