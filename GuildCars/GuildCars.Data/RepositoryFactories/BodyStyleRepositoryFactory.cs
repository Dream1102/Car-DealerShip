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
    public static class BodyStyleRepositoryFactory
    {
        public static IBodyStyleRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                  return new BodyStyleRepositoryQA();
                case "Prod":
                    return new BodyStyleRepositoryADO();
                default:
                    throw new Exception("Could not find valid Repository Type configuration value.");
            }
        }
    }
}
