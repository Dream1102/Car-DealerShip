using GuildCars.Data.Interfaces.VehicleTables;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.ADO.VehicleTables
{
    public class VehicleRepositoryADO : IVehicleRepository
    {
        public void DeleteVehicle(string vehicleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<InventoryReportItem> GetInventoryReport()
        {
            List<InventoryReportItem> inventoryData = new List<InventoryReportItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("InventoryReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        InventoryReportItem currentRow = new InventoryReportItem();
                        currentRow.Year = (int)dr["Year"];
                        currentRow.Make = dr["MakeName"].ToString();
                        currentRow.Model = dr["ModelName"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (decimal)dr["StockValue"];
                        currentRow.IsUsed = (bool)dr["Used"];

                        inventoryData.Add(currentRow);
                    }
                }
                return inventoryData;
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AllInventorySearch", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        Vehicle currentRow = new Vehicle();
                        currentRow.VehicleId = dr["VehicleId"].ToString();
                        currentRow.VehicleModel.ModelName = dr["ModelName"].ToString();
                        currentRow.VehicleModel.Make.MakeName = dr["MakeName"].ToString();
                        currentRow.InteriorColor.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.ExteriorColor.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.TransmissionType.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.BodyStyle.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.ListedPrice = (decimal)dr["ListedPrice"];
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.Description = dr["Description"].ToString();
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();
                        currentRow.IsFeatured = (bool)dr["IsFeatured"];
                        currentRow.IsUsed = (bool)dr["IsUsed"];
                        currentRow.IsSold = (bool)dr["IsSold"];
                        currentRow.UserEmail = dr["UserEmail"].ToString();

                        vehicles.Add(currentRow);
                    }
                }
            }
            return vehicles;
        }

        public Vehicle GetVehicleById(string vehicleId)
        {
            Vehicle vehicle = new Vehicle();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {

                SqlCommand cmd = new SqlCommand("VehicleDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        vehicle.Year = (int)dr["Year"];
                        vehicle.VehicleId = dr["VehicleId"].ToString();
                        vehicle.VehicleModel.ModelId = (int)dr["ModelId"];
                        vehicle.VehicleModel.ModelName = dr["ModelName"].ToString();
                        vehicle.VehicleModel.Make.MakeId = (int)dr["MakeId"];
                        vehicle.VehicleModel.Make.MakeName = dr["MakeName"].ToString();
                        vehicle.TransmissionType.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        vehicle.TransmissionType.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        vehicle.ExteriorColor.ExteriorColorId = (int)dr["ExteriorColorId"];
                        vehicle.ExteriorColor.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        vehicle.InteriorColor.InteriorColorId = (int)dr["InteriorColorId"];
                        vehicle.InteriorColor.InteriorColorName = dr["InteriorColorName"].ToString();
                        vehicle.BodyStyle.BodyStyleId = (int)dr["BodyStyleId"];
                        vehicle.BodyStyle.BodyStyleName = dr["BodyStyleName"].ToString();
                        vehicle.Mileage = (int)dr["Mileage"];
                        vehicle.ListedPrice = (decimal)dr["ListedPrice"];
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.ImageFileName = dr["ImageFileName"].ToString();
                        vehicle.Description = dr["Description"].ToString();
                        vehicle.IsFeatured = (bool)dr["IsFeatured"];
                        vehicle.IsUsed = (bool)dr["IsUsed"];
                        vehicle.IsSold = (bool)dr["IsSold"];
                        vehicle.UserEmail = dr["UserEmail"].ToString();
                    }
                }
                return vehicle;
            }
        }

        public List<Vehicle> GetFeaturedVehicles()
        {
            List<Vehicle> featured = new List<Vehicle>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("FeaturedSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Vehicle currentRow = new Vehicle();
                        currentRow.VehicleId = dr["VehicleId"].ToString();
                        currentRow.VehicleModel.ModelName = dr["ModelName"].ToString();
                        currentRow.VehicleModel.Make.MakeName = dr["MakeName"].ToString();
                        currentRow.ListedPrice = (decimal)dr["ListedPrice"];
                        currentRow.Year = (int)dr["Year"];
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        featured.Add(currentRow);
                    }
                }
            }
            return featured;
        }

        public void InsertVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.VehicleModel.ModelId);
                cmd.Parameters.AddWithValue("@IsUsed", vehicle.IsUsed);
                cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyle.BodyStyleId);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@TransmissionTypeId", vehicle.TransmissionType.TransmissionTypeId);
                cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColor.ExteriorColorId);
                cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColor.InteriorColorId);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@ListedPrice", vehicle.ListedPrice);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@ImageFileName", vehicle.ImageFileName);
                cmd.Parameters.AddWithValue("@IsFeatured", vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@IsSold", vehicle.IsSold);
                cmd.Parameters.AddWithValue("@UserEmail", vehicle.UserEmail);
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModelId", vehicle.VehicleModel.ModelId);
                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColor.InteriorColorId);
                cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColor.ExteriorColorId);
                cmd.Parameters.AddWithValue("@TransmissionTypeId", vehicle.TransmissionType.TransmissionTypeId);
                cmd.Parameters.AddWithValue("@BodyStyleId", vehicle.BodyStyle.BodyStyleId);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@ListedPrice", vehicle.ListedPrice);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);
                cmd.Parameters.AddWithValue("@ImageFileName", vehicle.ImageFileName);
                cmd.Parameters.AddWithValue("@IsFeatured", vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@IsUsed", vehicle.IsUsed);
                cmd.Parameters.AddWithValue("@IsSold", vehicle.IsSold);
                cmd.Parameters.AddWithValue("@UserEmail", vehicle.UserEmail);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<Vehicle> SalesUserVehicleSearch(SalesVehicleSearchParameters parameters)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            decimal minPrice = parameters.MinPrice.HasValue ? parameters.MinPrice.Value : 0;
            decimal maxPrice = parameters.MaxPrice.HasValue ? parameters.MaxPrice.Value : 999999;
            int minYear = parameters.MinYear.HasValue ? parameters.MinYear.Value : 1920;
            int maxYear = parameters.MaxYear.HasValue ? parameters.MaxYear.Value : 2500;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SalesVehicleQuickSearch", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!String.IsNullOrEmpty(parameters.SearchParameter))
                {
                    cmd.Parameters.AddWithValue("@SearchParameter", parameters.SearchParameter);
                }

                cmd.Parameters.AddWithValue("@MinPrice", minPrice);
                cmd.Parameters.AddWithValue("@MaxPrice", maxPrice);
                cmd.Parameters.AddWithValue("@MinYear", minYear);
                cmd.Parameters.AddWithValue("@MaxYear", maxYear);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        Vehicle currentRow = new Vehicle();
                        currentRow.VehicleId = dr["VehicleId"].ToString();
                        currentRow.Year = (int)dr["Year"];
                        currentRow.VehicleModel.ModelName = dr["ModelName"].ToString();
                        currentRow.VehicleModel.Make.MakeName = dr["MakeName"].ToString();
                        currentRow.BodyStyle.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionType.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.ExteriorColor.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.InteriorColor.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.ListedPrice = (decimal)dr["ListedPrice"];
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        vehicles.Add(currentRow);
                    }
                }
            }
            return vehicles;
        }

        public List<Vehicle> AnonymousUserVehicleSearch(VehicleSearchParameters parameters)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            decimal minPrice = parameters.MinPrice.HasValue ? parameters.MinPrice.Value : 0;
            decimal maxPrice = parameters.MaxPrice.HasValue ? parameters.MaxPrice.Value : 999999;
            decimal minYear = parameters.MinYear.HasValue ? parameters.MinYear.Value : 1920;
            decimal maxYear = parameters.MaxYear.HasValue ? parameters.MaxYear.Value : 2500;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleQuickSearch", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!String.IsNullOrEmpty(parameters.SearchParameter))
                {
                    cmd.Parameters.AddWithValue("@SearchParameter", parameters.SearchParameter);
                }

                cmd.Parameters.AddWithValue("@MinPrice", minPrice);
                cmd.Parameters.AddWithValue("@MaxPrice", maxPrice);
                cmd.Parameters.AddWithValue("@MinYear", minYear);
                cmd.Parameters.AddWithValue("@MaxYear", maxYear);
                cmd.Parameters.AddWithValue("@IsUsed", parameters.IsUsed);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        Vehicle currentRow = new Vehicle();
                        currentRow.VehicleId = dr["VehicleId"].ToString();
                        currentRow.Year = (int)dr["Year"];
                        currentRow.VehicleModel.ModelName = dr["ModelName"].ToString();
                        currentRow.VehicleModel.Make.MakeName = dr["MakeName"].ToString();
                        currentRow.BodyStyle.BodyStyleName = dr["BodyStyleName"].ToString();
                        currentRow.TransmissionType.TransmissionTypeName = dr["TransmissionTypeName"].ToString();
                        currentRow.ExteriorColor.ExteriorColorName = dr["ExteriorColorName"].ToString();
                        currentRow.InteriorColor.InteriorColorName = dr["InteriorColorName"].ToString();
                        currentRow.Mileage = (int)dr["Mileage"];
                        currentRow.ListedPrice = (decimal)dr["ListedPrice"];
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.ImageFileName = dr["ImageFileName"].ToString();

                        vehicles.Add(currentRow);
                    }
                }
            }
            return vehicles;
        }

        public List<SelectListItem> GetYearsOfVehiclesInInventory(bool isUsed)
        {
            var searchParams = new VehicleSearchParameters();
            searchParams.IsUsed = isUsed;
            var vehicles = AnonymousUserVehicleSearch(searchParams).OrderBy(y => y.Year);

            List<int> vehicleYears = new List<int>();

            foreach(var vehicle in vehicles)
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


        public List<SelectListItem> GetYearsOfVehiclesInInventory()
        {
            var searchParams = new SalesVehicleSearchParameters();
            var vehicles = SalesUserVehicleSearch(searchParams).OrderBy(y => y.Year);

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
        public List<SelectListItem> GetListPricesOfVehiclesInInventory(bool isUsed)
        {
            var searchParams = new VehicleSearchParameters();
            searchParams.IsUsed = isUsed;
            var vehicles = AnonymousUserVehicleSearch(searchParams).OrderByDescending(m => m.MSRP);

            List<decimal> listPrices = new List<decimal>();

            foreach (var vehicle in vehicles)
            {
                if (!listPrices.Contains(vehicle.ListedPrice))
                {
                    listPrices.Add(vehicle.ListedPrice);
                }
            }

            List<SelectListItem> items = listPrices.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.ToString(),
                    Value = p.ToString(),
                    Selected = false
                };
            });

            return items;

        }

        public List<SelectListItem> GetListPricesOfVehiclesInInventory()
        {
            var searchParams = new SalesVehicleSearchParameters();
            var vehicles = SalesUserVehicleSearch(searchParams).OrderByDescending(m => m.MSRP);

            List<decimal> listPrices = new List<decimal>();

            foreach (var vehicle in vehicles)
            {
                if (!listPrices.Contains(vehicle.ListedPrice))
                {
                    listPrices.Add(vehicle.ListedPrice);
                }
            }

            List<SelectListItem> items = listPrices.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.ToString(),
                    Value = p.ToString(),
                    Selected = false
                };
            });

            return items;
        }

    }
}
