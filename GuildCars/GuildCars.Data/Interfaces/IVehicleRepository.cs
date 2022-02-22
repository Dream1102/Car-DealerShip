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
        List<Vehicle> GetFeaturedVehicles();
        List<Vehicle> AnonymousUserVehicleSearch(VehicleSearchParameters parameters);
        List<Vehicle> SalesUserVehicleSearch(SalesVehicleSearchParameters parameters);
        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(string vehicleId);
        void InsertVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(string vehicleId);
        List<SelectListItem> GetYearsOfVehiclesInInventory(bool isUsed);
        List<SelectListItem> GetListPricesOfVehiclesInInventory(bool isUsed);
        List<SelectListItem> GetYearsOfVehiclesInInventory();
        List<SelectListItem> GetListPricesOfVehiclesInInventory();
        List<InventoryReportItem> GetInventoryReport();
    }
}
