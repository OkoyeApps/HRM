using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
   public interface IPrefixes
    {
        void addprefixes(Prefixes prefix);
        void DeletePrefixes(int id);
        IEnumerable<Prefixes> GetPrefixes();
        Prefixes GetPrefixesById(int id); 
    }
}
