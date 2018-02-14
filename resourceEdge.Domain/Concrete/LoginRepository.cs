using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public  class LoginRepository : ILogin
    {
        UnitofWork.UnitOfWork unitofWork = new UnitofWork.UnitOfWork();
        public void Delete(int id)
        {
            unitofWork.Logins.Delete(id);
            unitofWork.Save();
        }

        public IEnumerable<Logins> Get()
        {
            throw new NotImplementedException();
        }

        public Logins GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Logins GetByUserId(string userId)
        {
            var AllLogin = unitofWork.Logins.Get();
            var CurrentLogin = AllLogin.Where(x => x.userId == userId && x.LogOutTime == null && x.IsLogOut == false).FirstOrDefault();
            return CurrentLogin;
        }

        public EdgeDbContext GetDbContext()
        {
            return unitofWork.GetDbContext();
        }

        public void Insert(Logins entity)
        {
            unitofWork.Logins.Insert(entity);
            unitofWork.Save();
        }

        public void update(Logins entity)
        {
            unitofWork.Logins.Update(entity);
            unitofWork.Save();
        }
    }
}
