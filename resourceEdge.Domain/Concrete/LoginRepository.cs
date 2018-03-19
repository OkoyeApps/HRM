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

        public IEnumerable<Login> Get()
        {
            throw new NotImplementedException();
        }

        public Login GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Login GetByUserId(string userId)
        {
            var AllUserLogin = unitofWork.Logins.Get(x => x.userId == userId && x.LogOutTime == null && x.IsLogOut == false).FirstOrDefault();
            return AllUserLogin; 
        }

        public EdgeDbContext GetDbContext()
        {
            return unitofWork.GetDbContext();
        }

        public void Insert(Login entity)
        {
            unitofWork.Logins.Insert(entity);
            unitofWork.Save();
        }

        public void update(Login entity)
        {
            unitofWork.Logins.Update(entity);
            unitofWork.Save();
        }
        public Login GetUserLastLogin(string userId)
        {
            var user = unitofWork.Logins.Get(filter: x => x.userId == userId).ToList().LastOrDefault();
            return user ?? null;
        }

    }
}
