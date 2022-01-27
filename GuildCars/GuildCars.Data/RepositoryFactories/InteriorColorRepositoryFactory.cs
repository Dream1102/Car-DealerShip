using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Data.Repositories.ADO.VehicleTables;
using GuildCars.Data.Repositories.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.RepositoryFactories
{
    public static class InteriorColorRepositoryFactory
    {
        public static IInteriorColorRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                  return new InteriorColorRepositoryQA();
                case "Prod":
                    return new InteriorColorRepositoryADO();
                default:
                    throw new Exception("Could not find valid Repository Type configuration value.");
            }
        }
    }
}
