using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GuildCars.Data.Interfaces.VehicleTables
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetFeatured();
        List<Vehicle> VehicleSearch(VehicleSearchParameters parameters);
        List<Vehicle> SalesVehicleSearch(SalesVehicleSearchParameters parameters);
        List<Vehicle> GetAllVehicles();
        Vehicle GetById(string vehicleId);
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Delete(string vehicleId);
        List<SelectListItem> GetYears(bool isUsed);
        List<SelectListItem> GetListPrices(bool isUsed);
        List<SelectListItem> GetYears();
        List<SelectListItem> GetListPrices();
        List<InventoryReportItem> GetInventoryReport();
    }
}
