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

    public enum MailType
    {
        Account =1, Appraisal
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
    public enum EnableTo
    {
        Manager = 1, Employee
    }
    public enum AppraisalMode
    {
        Quarterly = 1, Half_Yearly, Yearly
    }
    public enum AppraisalStatus
    {
        Open = 1, Closed, In_Progress
    }
    public enum JobFrequency
    {
        Monthly = 1, Yearly, Daily, Custom
    }
    public enum Verdict
    {
        Guilty = 1, Not_Guilty
    }
    public enum AppraisalQuestionType
    {
        Personal =1,
        general, Department
    }
}
