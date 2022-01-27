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
    public static class ContactRequestRepositoryFactory
    {
        public static IContactRequestRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "QA":
                  return new ContactRequestRepositoryQA();
                case "Prod":
                    return new ContactRequestRepositoryADO();
                default:
                    throw new Exception("Could not find valid Repository Type configuration value.");
            }
        }
    }
}
