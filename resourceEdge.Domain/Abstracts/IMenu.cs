﻿using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
   public  interface IMenu
    {
        IEnumerable<Menu> Get();
        void ActivateMenu(int id);
        void DeActivateMenu(int id);
    }
}