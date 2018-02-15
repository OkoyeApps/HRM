using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
    public interface IFiles : GenericInterface<Files>
    {
        Files GetFileByUserId(string userId, FileType type);
    }
}
