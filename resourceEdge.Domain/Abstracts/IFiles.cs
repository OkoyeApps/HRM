﻿using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IFiles : GenericInterface<File>
    {
        File GetFileByUserId(string userId, FileType type);
    }
}
