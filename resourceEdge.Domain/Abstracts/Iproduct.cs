using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Abstracts
{
   public interface Iproduct
    {
        IEnumerable<products> product { get; }
        void Addproducts(products product);
    }
}
