using resourceEdge.Domain.Entities;
using resourceEdge.Domain.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace resourceEdge.webUi.Infrastructure
{
    public class AppraisalManager
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public string InitializationCodeGeneration(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(InitializationCodeGeneration(4, true));
            builder.Append(InitializationCodeGeneration(2, false));
            return builder.ToString();
        }

        public AppraisalPeriods GetPeriodByName(string name)
        {
            var period = unitOfWork.AppraisalPeriod.Get(filter: x => x.Name == name).FirstOrDefault();
            return period ?? null;
        }
        public List<AppraisalInitialization> GetAllInitialization()
        {
            var result = unitOfWork.AppraisalInitialization.Get(includeProperties: "Group").ToList();
            return result ?? null;
        }
        public bool EnableAppraisal(int id)
        {
            var result = unitOfWork.AppraisalInitialization.GetByID(id);
            try
            {
                if (result != null)
                {
                    result.Enable = true;
                    unitOfWork.AppraisalInitialization.Update(result);
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return false;
        
        }

    }
}