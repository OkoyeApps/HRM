using resourceEdge.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resourceEdge.webUi.Infrastructure
{
    public class ConfigurationManager
    {
        private IBusinessUnits UnitRepo;
        public ConfigurationManager()
        {

        }
        public ConfigurationManager(IBusinessUnits UParam)
        {
            UnitRepo = UParam;
        }

        public bool DoesUnitExstInLocation(int locationId, string LocationName)
        {
            var location = UnitRepo.DoesUnitExitByName(LocationName); //This code checks if the Busines sunit already exist in the db and then if it doesnt
            if (location != null && location.LocationId == locationId) //If it does it check if it has the same location
            {
                return true;
            }
            return false;
        }
    }
}