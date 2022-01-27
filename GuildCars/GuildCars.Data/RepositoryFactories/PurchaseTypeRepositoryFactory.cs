using GuildCars.Data.Interfaces;
using GuildCars.Data.Repositories.ADO;
using GuildCars.Data.Repositories.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.RepositoryFactories
{
    public static class PurchaseTypeRepositoryFactory
    {
        public static IPurchaseTypeRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                    return new PurchaseTypeRepositoryQA();
                case "Prod":
                    return new PurchaseTypeRepositoryADO();
                default:
                    throw new Exception("Could not find valid Repository Type configuration value.");
            }
        }
    }
}
