using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GuildCars.Data.Repositories.QA
{
    public class VehicleRepositoryQA : IVehicleRepository
    {
        static DateTime date = new DateTime(2021, 11, 29);
        private List<Vehicle> _vehicles = new List<Vehicle>();

        public VehicleRepositoryQA()
        {
            SeedVehicles();
        }

        private void SeedVehicles()
        {
            Vehicle vehicle1 = new Vehicle();
            vehicle1.VehicleId = "1C4SDJGJ9JC416162";
            vehicle1.VehicleModel.ModelId = 10;
            vehicle1.VehicleModel.ModelName = "Durango";
            vehicle1.VehicleModel.Make.MakeId = 5;
            vehicle1.VehicleModel.Make.MakeName = "Dodge";
            vehicle1.InteriorColor.InteriorColorId = 1;
            vehicle1.InteriorColor.InteriorColorName = "Black";
            vehicle1.ExteriorColor.ExteriorColorId = 1;
            vehicle1.ExteriorColor.ExteriorColorName = "Black";
            vehicle1.TransmissionType.TransmissionTypeId = 1;
            vehicle1.TransmissionType.TransmissionTypeName = "Automatic";
            vehicle1.BodyStyle.BodyStyleId = 2;
            vehicle1.BodyStyle.BodyStyleName = "SUV";
            vehicle1.Mileage = 11503;
            vehicle1.Year = 2018;
            vehicle1.DateAdded = date;
            vehicle1.ListedPrice = 69900;
            vehicle1.MSRP = 72000;
            vehicle1.Description = "A really nice looking vehicle";
            vehicle1.ImageFileName = "inventory-1C4SDJGJ9JC416162.jpg";
            vehicle1.IsFeatured = true;
            vehicle1.IsUsed = true;
            vehicle1.IsSold = false;
            vehicle1.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle1);

            Vehicle vehicle2 = new Vehicle();
            vehicle2.VehicleId = "1C4SDHCT2HC833522";
            vehicle2.VehicleModel.ModelId = 10;
            vehicle2.VehicleModel.ModelName = "Durango";
            vehicle2.VehicleModel.Make.MakeId = 5;
            vehicle2.VehicleModel.Make.MakeName = "Dodge";
            vehicle2.InteriorColor.InteriorColorId = 2;
            vehicle2.InteriorColor.InteriorColorName = "Guild Red";
            vehicle2.ExteriorColor.ExteriorColorId = 3;
            vehicle2.ExteriorColor.ExteriorColorName = "Guild Gray";
            vehicle2.TransmissionType.TransmissionTypeId = 1;
            vehicle2.TransmissionType.TransmissionTypeName = "Automatic";
            vehicle2.BodyStyle.BodyStyleId = 2;
            vehicle2.BodyStyle.BodyStyleName = "SUV";
            vehicle2.Mileage = 85122;
            vehicle2.Year = 2017;
            vehicle2.DateAdded = date;
            vehicle2.ListedPrice = 35900;
            vehicle2.MSRP = 38000;
            vehicle2.Description = "A really nice looking used vehicle";
            vehicle2.ImageFileName = "inventory-1C4SDHCT2HC833522.jpg";
            vehicle2.IsFeatured = false;
            vehicle2.IsUsed = true;
            vehicle2.IsSold = false;
            vehicle2.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle2);

            Vehicle vehicle3 = new Vehicle();
            vehicle3.VehicleId = "2C3CDZAG3GH124663";
            vehicle3.VehicleModel.ModelId = 9;
            vehicle3.VehicleModel.ModelName = "Challenger";
            vehicle3.VehicleModel.Make.MakeId = 5;
            vehicle3.VehicleModel.Make.MakeName = "Dodge";
            vehicle3.InteriorColor.InteriorColorId = 2;
            vehicle3.InteriorColor.InteriorColorName = "Guild Red";
            vehicle3.ExteriorColor.ExteriorColorId = 3;
            vehicle3.ExteriorColor.ExteriorColorName = "Guild Gray";
            vehicle3.TransmissionType.TransmissionTypeId = 2;
            vehicle3.TransmissionType.TransmissionTypeName = "Manual";
            vehicle3.BodyStyle.BodyStyleId = 1;
            vehicle3.BodyStyle.BodyStyleName = "Car";
            vehicle3.Mileage = 105193;
            vehicle3.Year = 2016;
            vehicle3.DateAdded = date;
            vehicle3.ListedPrice = 21900;
            vehicle3.MSRP = 24000;
            vehicle3.Description = "A really fast blue car!";
            vehicle3.ImageFileName = "inventory-2C3CDZAG3GH124663.jpg";
            vehicle3.IsFeatured = false;
            vehicle3.IsUsed = true;
            vehicle3.IsSold = false;
            vehicle3.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle3);

            Vehicle vehicle4 = new Vehicle();
            vehicle4.VehicleId = "2C3CDZGG7HH628383";
            vehicle4.VehicleModel.ModelId = 9;
            vehicle4.VehicleModel.ModelName = "Challenger";
            vehicle4.VehicleModel.Make.MakeId = 5;
            vehicle4.VehicleModel.Make.MakeName = "Dodge";
            vehicle4.InteriorColor.InteriorColorId = 3;
            vehicle4.InteriorColor.InteriorColorName = "Guild Gray";
            vehicle4.ExteriorColor.ExteriorColorId = 5;
            vehicle4.ExteriorColor.ExteriorColorName = "Silver";
            vehicle4.TransmissionType.TransmissionTypeId = 2;
            vehicle4.TransmissionType.TransmissionTypeName = "Manual";
            vehicle4.BodyStyle.BodyStyleId = 1;
            vehicle4.BodyStyle.BodyStyleName = "Car";
            vehicle4.Mileage = 96;
            vehicle4.Year = 2021;
            vehicle4.DateAdded = date;
            vehicle4.ListedPrice = 36900;
            vehicle4.MSRP = 40000;
            vehicle4.Description = "A fast silver car!";
            vehicle4.ImageFileName = "inventory-2C3CDZGG7HH628383.jpg";
            vehicle4.IsFeatured = true;
            vehicle4.IsUsed = false;
            vehicle4.IsSold = false;
            vehicle4.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle4);

            Vehicle vehicle5 = new Vehicle();
            vehicle5.VehicleId = "2C4RDGDG4GR321504";
            vehicle5.VehicleModel.ModelId = 11;
            vehicle5.VehicleModel.ModelName = "Grand Caravan";
            vehicle5.VehicleModel.Make.MakeId = 5;
            vehicle5.VehicleModel.Make.MakeName = "Dodge";
            vehicle5.InteriorColor.InteriorColorId = 1;
            vehicle5.InteriorColor.InteriorColorName = "Black";
            vehicle5.ExteriorColor.ExteriorColorId = 3;
            vehicle5.ExteriorColor.ExteriorColorName = "Guild Gray";
            vehicle5.TransmissionType.TransmissionTypeId = 1;
            vehicle5.TransmissionType.TransmissionTypeName = "Automatic";
            vehicle5.BodyStyle.BodyStyleId = 4;
            vehicle5.BodyStyle.BodyStyleName = "Van";
            vehicle5.Mileage = 103235;
            vehicle5.Year = 2016;
            vehicle5.DateAdded = date;
            vehicle5.ListedPrice = 14900;
            vehicle5.MSRP = 16000;
            vehicle5.Description = "A really useful van";
            vehicle5.ImageFileName = "inventory-2C4RDGDG4GR321504.jpg";
            vehicle5.IsFeatured = false;
            vehicle5.IsUsed = true;
            vehicle5.IsSold = false;
            vehicle5.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle5);

            Vehicle vehicle6 = new Vehicle();
            vehicle6.VehicleId = "1FM5K8GTXGGB60169";
            vehicle6.VehicleModel.ModelId = 2;
            vehicle6.VehicleModel.ModelName = "Explorer";
            vehicle6.VehicleModel.Make.MakeId = 1;
            vehicle6.VehicleModel.Make.MakeName = "Ford";
            vehicle6.InteriorColor.InteriorColorId = 3;
            vehicle6.InteriorColor.InteriorColorName = "Guild Gray";
            vehicle6.ExteriorColor.ExteriorColorId = 2;
            vehicle6.ExteriorColor.ExteriorColorName = "Guild Red";
            vehicle6.TransmissionType.TransmissionTypeId = 1;
            vehicle6.TransmissionType.TransmissionTypeName = "Automatic";
            vehicle6.BodyStyle.BodyStyleId = 2;
            vehicle6.BodyStyle.BodyStyleName = "SUV";
            vehicle6.Mileage = 125884;
            vehicle6.Year = 2016;
            vehicle6.DateAdded = date;
            vehicle6.ListedPrice = 27900;
            vehicle6.MSRP = 31000;
            vehicle6.Description = "A used......Explorer";
            vehicle6.ImageFileName = "inventory-1FM5K8GTXGGB60169.jpg";
            vehicle6.IsFeatured = true;
            vehicle6.IsUsed = true;
            vehicle6.IsSold = false;
            vehicle6.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle6);

            Vehicle vehicle7 = new Vehicle();
            vehicle7.VehicleId = "1FMHK7F83BGA02897";
            vehicle7.VehicleModel.ModelId = 2;
            vehicle7.VehicleModel.ModelName = "Explorer";
            vehicle7.VehicleModel.Make.MakeId = 1;
            vehicle7.VehicleModel.Make.MakeName = "Ford";
            vehicle7.InteriorColor.InteriorColorId = 2;
            vehicle7.InteriorColor.InteriorColorName = "Guild Red";
            vehicle7.ExteriorColor.ExteriorColorId = 3;
            vehicle7.ExteriorColor.ExteriorColorName = "Guild Gray";
            vehicle7.TransmissionType.TransmissionTypeId = 1;
            vehicle7.TransmissionType.TransmissionTypeName = "Automatic";
            vehicle7.BodyStyle.BodyStyleId = 2;
            vehicle7.BodyStyle.BodyStyleName = "SUV";
            vehicle7.Mileage = 50;
            vehicle7.Year = 2019;
            vehicle7.DateAdded = date;
            vehicle7.ListedPrice = 55000;
            vehicle7.MSRP = 57000;
            vehicle7.Description = "A very new......Explorer";
            vehicle7.ImageFileName = "inventory-1FMHK7F83BGA02897.jpg";
            vehicle7.IsFeatured = false;
            vehicle7.IsUsed = false;
            vehicle7.IsSold = true;
            vehicle7.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle7);

            Vehicle vehicle8 = new Vehicle();
            vehicle8.VehicleId = "1FM5K8F88DGB36574";
            vehicle8.VehicleModel.ModelId = 5;
            vehicle8.VehicleModel.ModelName = "ForeRunner";
            vehicle8.VehicleModel.Make.MakeId = 3;
            vehicle8.VehicleModel.Make.MakeName = "Toyota";
            vehicle8.InteriorColor.InteriorColorId = 2;
            vehicle8.InteriorColor.InteriorColorName = "Guild Red";
            vehicle8.ExteriorColor.ExteriorColorId = 3;
            vehicle8.ExteriorColor.ExteriorColorName = "Guild Gray";
            vehicle8.TransmissionType.TransmissionTypeId = 1;
            vehicle8.TransmissionType.TransmissionTypeName = "Automatic";
            vehicle8.BodyStyle.BodyStyleId = 2;
            vehicle8.BodyStyle.BodyStyleName = "SUV";
            vehicle8.Mileage = 85122;
            vehicle8.Year = 2017;
            vehicle8.DateAdded = date;
            vehicle8.ListedPrice = 35900;
            vehicle8.MSRP = 38000;
            vehicle8.Description = "A really nice looking used vehicle";
            vehicle8.ImageFileName = "inventory-1FM5K8F88DGB36574.jpg";
            vehicle8.IsFeatured = false;
            vehicle8.IsUsed = true;
            vehicle8.IsSold = false;
            vehicle8.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle8);

            Vehicle vehicle9 = new Vehicle();
            vehicle9.VehicleId = "NM0LS6BN3CT075262";
            vehicle9.VehicleModel.ModelId = 5;
            vehicle9.VehicleModel.ModelName = "ForeRunner";
            vehicle9.VehicleModel.Make.MakeId = 3;
            vehicle9.VehicleModel.Make.MakeName = "Toyota";
            vehicle9.InteriorColor.InteriorColorId = 2;
            vehicle9.InteriorColor.InteriorColorName = "Guild Red";
            vehicle9.ExteriorColor.ExteriorColorId = 3;
            vehicle9.ExteriorColor.ExteriorColorName = "Guild Gray";
            vehicle9.TransmissionType.TransmissionTypeId = 1;
            vehicle9.TransmissionType.TransmissionTypeName = "Automatic";
            vehicle9.BodyStyle.BodyStyleId = 2;
            vehicle9.BodyStyle.BodyStyleName = "SUV";
            vehicle9.Mileage = 85122;
            vehicle9.Year = 2017;
            vehicle9.DateAdded = date;
            vehicle9.ListedPrice = 35900;
            vehicle9.MSRP = 38000;
            vehicle9.Description = "A really nice looking used vehicle";
            vehicle9.ImageFileName = "inventory-NM0LS6BN3CT075262.jpg";
            vehicle9.IsFeatured = false;
            vehicle9.IsUsed = true;
            vehicle9.IsSold = false;
            vehicle9.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle9);

            Vehicle vehicle10 = new Vehicle();
            vehicle10.VehicleId = "1FMEE5BH1MLA96289";
            vehicle10.VehicleModel.ModelId = 5;
            vehicle10.VehicleModel.ModelName = "ForeRunner";
            vehicle10.VehicleModel.Make.MakeId = 3;
            vehicle10.VehicleModel.Make.MakeName = "Toyota";
            vehicle10.InteriorColor.InteriorColorId = 2;
            vehicle10.InteriorColor.InteriorColorName = "Guild Red";
            vehicle10.ExteriorColor.ExteriorColorId = 3;
            vehicle10.ExteriorColor.ExteriorColorName = "Guild Gray";
            vehicle10.TransmissionType.TransmissionTypeId = 1;
            vehicle10.TransmissionType.TransmissionTypeName = "Automatic";
            vehicle10.BodyStyle.BodyStyleId = 2;
            vehicle10.BodyStyle.BodyStyleName = "SUV";
            vehicle10.InteriorColor.InteriorColorId = 2;
            vehicle10.ExteriorColor.ExteriorColorId = 3;
            vehicle10.TransmissionType.TransmissionTypeId = 1;
            vehicle10.BodyStyle.BodyStyleId = 2;
            vehicle10.Mileage = 100;
            vehicle10.Year = 2021;
            vehicle10.DateAdded = date;
            vehicle10.ListedPrice = 35900;
            vehicle10.MSRP = 38000;
            vehicle10.Description = "A really nice looking used vehicle";
            vehicle10.ImageFileName = "inventory-1FMEE5BH1MLA96289.jpg";
            vehicle10.IsFeatured = false;
            vehicle10.IsUsed = false;
            vehicle10.IsSold = false;
            vehicle10.UserEmail = "jamie@guildcars.com";
            _vehicles.Add(vehicle10);
        }

        public void DeleteVehicle(string vehicleId)
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            foreach(var vehicle in _vehicles)
            {
                if(vehicle.VehicleId != vehicleId)
                {
                    vehicles.Add(vehicle);
                }
            }
            _vehicles = vehicles;
            
        }

        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            foreach (var vehicle in _vehicles)
            {
                vehicles.Add(vehicle);
            }

            return vehicles;
        }

        public Vehicle GetVehicleById(string vehicleId)
        {
            foreach(var vehicle in _vehicles)
            {
                if(vehicle.VehicleId == vehicleId)
                {
                    return vehicle;
                }
            }
            return null;
        }

        public List<Vehicle> GetFeaturedVehicles()
        {
            List<Vehicle> featuredVehicles = new List<Vehicle>();

            foreach(var vehicle in _vehicles)
            {
                if(vehicle.IsFeatured == true)
                {
                    featuredVehicles.Add(vehicle);
                }
            }
            return featuredVehicles;
        }

        public void InsertVehicle(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            vehicle = _vehicles.Where(v => v.VehicleId == vehicle.VehicleId).FirstOrDefault();
        }

        public List<Vehicle> AnonymousUserVehicleSearch(VehicleSearchParameters parameters)
        {
            decimal minPrice = 0;
            decimal maxPrice = 999999;
            int minYear = 1920;
            int maxYear = 3000;

            if(parameters.MinPrice.HasValue || parameters.MaxPrice.HasValue)
            {
                if(!parameters.MaxPrice.HasValue)
                {
                    parameters.MaxPrice = maxPrice;
                }
                if (!parameters.MinPrice.HasValue)
                {
                    parameters.MinPrice = minPrice;
                }
            }
            else
            {
                parameters.MinPrice = minPrice;
                parameters.MaxPrice = maxPrice;
            }

            if(parameters.MinYear.HasValue || parameters.MaxYear.HasValue)
            {
                if (!parameters.MaxYear.HasValue)
                {
                    parameters.MaxYear = maxYear;
                }
                if (!parameters.MinYear.HasValue)
                {
                    parameters.MinYear = minYear;
                }
            }
            else
            {
                parameters.MinYear = minYear;
                parameters.MaxYear = maxYear;
            }

            if (string.IsNullOrEmpty(parameters.SearchParameter))
            {
                List<Vehicle> searchResults = (from vehicle in _vehicles
                                               where vehicle.IsUsed == parameters.IsUsed
                                               where vehicle.IsSold == parameters.IsSold
                                               where vehicle.ListedPrice >= parameters.MinPrice && vehicle.ListedPrice <= parameters.MaxPrice && vehicle.Year >= parameters.MinYear && vehicle.Year <= parameters.MaxYear
                                               select vehicle).ToList();
                return searchResults;
            }
            else
            {
                List<Vehicle> searchResults = (from vehicle in _vehicles
                                               where vehicle.IsUsed == parameters.IsUsed
                                               where vehicle.IsSold == parameters.IsSold
                                               where vehicle.VehicleModel.ModelName.Contains(parameters.SearchParameter) || vehicle.VehicleModel.Make.MakeName.Contains(parameters.SearchParameter) || vehicle.Year.ToString().Contains(parameters.SearchParameter)
                                               where vehicle.ListedPrice >= parameters.MinPrice && vehicle.ListedPrice <= parameters.MaxPrice && vehicle.Year >= parameters.MinYear && vehicle.Year <= parameters.MaxYear
                                               select vehicle).ToList();
                return searchResults;
            } 
        }

        public List<Vehicle> SalesUserVehicleSearch(SalesVehicleSearchParameters parameters)
        {
            decimal minPrice = 0;
            decimal maxPrice = 999999;
            int minYear = 1920;
            int maxYear = 3000;

            if (parameters.MinPrice.HasValue || parameters.MaxPrice.HasValue)
            {
                if (!parameters.MaxPrice.HasValue)
                {
                    parameters.MaxPrice = maxPrice;
                }
                if (!parameters.MinPrice.HasValue)
                {
                    parameters.MinPrice = minPrice;
                }
            }
            else
            {
                parameters.MinPrice = minPrice;
                parameters.MaxPrice = maxPrice;
            }

            if (parameters.MinYear.HasValue || parameters.MaxYear.HasValue)
            {
                if (!parameters.MaxYear.HasValue)
                {
                    parameters.MaxYear = maxYear;
                }
                if (!parameters.MinYear.HasValue)
                {
                    parameters.MinYear = minYear;
                }
            }
            else
            {
                parameters.MinYear = minYear;
                parameters.MaxYear = maxYear;
            }

            if (string.IsNullOrEmpty(parameters.SearchParameter))
            {
                List<Vehicle> searchResults = (from vehicle in _vehicles
                                               where vehicle.IsSold == parameters.IsSold
                                               where vehicle.ListedPrice >= parameters.MinPrice && vehicle.ListedPrice <= parameters.MaxPrice && vehicle.Year >= parameters.MinYear && vehicle.Year <= parameters.MaxYear
                                               select vehicle).ToList();
                return searchResults;
            }
            else
            {
                List<Vehicle> searchResults = (from vehicle in _vehicles
                                               where vehicle.IsSold == parameters.IsSold
                                               where vehicle.VehicleModel.ModelName.Contains(parameters.SearchParameter) || vehicle.VehicleModel.Make.MakeName.Contains(parameters.SearchParameter) || vehicle.Year.ToString().Contains(parameters.SearchParameter)
                                               where vehicle.ListedPrice >= parameters.MinPrice && vehicle.ListedPrice <= parameters.MaxPrice && vehicle.Year >= parameters.MinYear && vehicle.Year <= parameters.MaxYear
                                               select vehicle).ToList();
                return searchResults;
            }
        }

        public List<InventoryReportItem> GetInventoryReport()
        {
            var inventoryReport = new List<InventoryReportItem>();
            var item = new InventoryReportItem();
            var vehicles = (from vehicle in _vehicles
                            group vehicle by new
                            {
                                Year = vehicle.Year,
                                Make = vehicle.VehicleModel.Make.MakeName,
                                Model = vehicle.VehicleModel.ModelName
                            } into grouping
                            orderby grouping.Key.Year descending
                            select new
                            {
                                Year = grouping.Key.Year,
                                Make = grouping.Key.Make,
                                Model = grouping.Key.Model,
                                Count = grouping.Count(),
                                StockValue = grouping.Sum(s => s.MSRP)
                            }).ToList();
                            
            foreach (var vehicle in vehicles)
            {
                item.Year = vehicle.Year;
                item.Make = vehicle.Make;
                item.Model = vehicle.Model;
                item.Count = vehicle.Count;
                item.StockValue = vehicle.StockValue;

                inventoryReport.Add(item);
            }
            return inventoryReport;                          
        }

        public List<SelectListItem> GetYearsOfVehiclesInInventory(bool isUsed)
        {
            var searchParams = new VehicleSearchParameters();
            searchParams.IsUsed = isUsed;
            var vehicles = AnonymousUserVehicleSearch(searchParams).OrderBy(y => y.Year);

            List<int> vehicleYears = new List<int>();

            foreach (var vehicle in vehicles)
            {
                if (!vehicleYears.Contains(vehicle.Year))
                {
                    vehicleYears.Add(vehicle.Year);
                }
            }

            List<SelectListItem> items = vehicleYears.ConvertAll(y =>
            {
                return new SelectListItem()
                {
                    Text = y.ToString(),
                    Value = y.ToString(),
                    Selected = false
                };
            });

            return items;
        }
    }
}
