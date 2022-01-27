using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class TransmissionTypeRepositoryQA : ITransmissionTypeRepository
    {
        private List<TransmissionType> _transmissionTypes = new List<TransmissionType>();

        public TransmissionTypeRepositoryQA()
        {
            SeedTransmissionTypes();
        }

        private void SeedTransmissionTypes()
        {
            TransmissionType type1 = new TransmissionType();
            type1.TransmissionTypeId = 1;
            type1.TransmissionTypeName = "Automatic";
            _transmissionTypes.Add(type1);

            TransmissionType type2 = new TransmissionType();
            type2.TransmissionTypeId = 2;
            type2.TransmissionTypeName = "Manual";
            _transmissionTypes.Add(type1);
        }

        public List<TransmissionType> GetTransmissionTypes()
        {
            List<TransmissionType> transmissionTypes = new List<TransmissionType>();

            foreach (var type in _transmissionTypes)
            {
                transmissionTypes.Add(type);
            }

            return transmissionTypes;
        }
    }
}
