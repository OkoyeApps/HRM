﻿using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using resourceEdge.Domain.Entities;

namespace resourceEdge.Domain.Concrete
{
    public class PositionRepository : IPositions
    {
        UnitofWork.UnitOfWork unitOfWork = new UnitofWork.UnitOfWork();
        public void Insert(Positions position)
        {
            unitOfWork.positions.Insert(position);
            unitOfWork.Save();
        }
        public void Delete(int id)
        {
            unitOfWork.positions.Delete(id);
            unitOfWork.Save();
        }
        public IEnumerable<Positions> Get() => unitOfWork.positions.Get();
        public Positions GetById(int id) => unitOfWork.positions.GetByID(id);

        public void update(Positions entity)
        {
            throw new NotImplementedException();
        }

        public Positions GetByUserId(string userId)
        {
            throw new NotImplementedException();
        }


    }
}
