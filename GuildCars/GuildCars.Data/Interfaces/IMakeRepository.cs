using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces.VehicleTables
{
    public interface IMakeRepository
    {
        List<Make> GetAllVehicleMakes();
        void InsertVehicleMake(Make make);
    }
}
