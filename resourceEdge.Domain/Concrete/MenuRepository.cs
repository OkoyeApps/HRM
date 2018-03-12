using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class MenuRepository : IMenu
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void ActivateMenu(int id)
        {
            var menu = unitOfWork.Menu.GetByID(id);
            if (menu != null)
            {
                menu.Active = true;
                unitOfWork.Menu.Update(menu);
                unitOfWork.Save();
            }
        }

        public void DeActivateMenu(int id)
        {
            var menu = unitOfWork.Menu.GetByID(id);
            if (menu != null)
            {
                menu.Active = false;
                unitOfWork.Menu.Update(menu);
                unitOfWork.Save();
            }
        }

        public IEnumerable<Menus> Get() => unitOfWork.Menu.Get();
    }
}
