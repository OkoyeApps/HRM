using resourceEdge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Abstracts
{
   public interface IJobtitles
    {
        void addJobTitles(Jobtitles jobs);
        IEnumerable<Jobtitles> GetJobTitles();
        Jobtitles GetJobTitlesById(int id);
        void deleteJobTitles(int id);
    }
}
