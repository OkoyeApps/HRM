using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resourceEdge.Domain.Infrastructures
{
    public enum prefixes
    {
        Mr,
        Mrs,
        Ms
    }
    public enum ModeOfEmployement
    {
        Direct,
        Interview,
        Other,
        Reference
    }
    public enum empStatus
    {
        Old, New
    }

    public enum Answers
    {
        YES =1, No
    }

    public enum Status
    {
        Approved = 1, Canceled
    }

    public enum Priority
    {
        High = 1, Low, Medium
    }
}
