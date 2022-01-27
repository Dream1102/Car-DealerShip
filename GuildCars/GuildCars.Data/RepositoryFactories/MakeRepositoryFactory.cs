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
    public static class MakeRepositoryFactory
    {
        public static IMakeRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                  return new MakeRepositoryQA();
                case "Prod":
                    return new MakeRepositoryADO();
                default:
                    throw new Exception("Could not find valid Repository Type configuration value.");
            }
        }
    }
}
