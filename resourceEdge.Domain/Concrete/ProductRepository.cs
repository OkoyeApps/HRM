using resourceEdge.Domain.Abstracts;
using resourceEdge.Domain.Concrete;
using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Concrete
{
    public class ProductRepository : Iproduct
    {
        UnitOfWork unitofwork = new UnitOfWork();
        public IEnumerable<products> product { get { return unitofwork.productRepository.Get(); } }

        public void Addproducts(products product)
        {
            unitofwork.productRepository.Insert(product);
            unitofwork.Save();
        }

        public IEnumerable<products> Get() => unitofwork.productRepository.Get();

    }
}

