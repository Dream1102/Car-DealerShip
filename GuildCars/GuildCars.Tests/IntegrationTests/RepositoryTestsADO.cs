using GuildCars.Data.Repositories.ADO;
using GuildCars.Data.Repositories.ADO.VehicleTables;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class RepositoryTestsADO
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
       

        //Vehicle Tests------------------------------------------------------------
        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStyleRepositoryADO();

            var bodyStyles = repo.GetBodyStyles();

            Assert.AreEqual(4, bodyStyles.Count);

            Assert.AreEqual(1, bodyStyles[0].BodyStyleId);
            Assert.AreEqual("Car", bodyStyles[0].BodyStyleName);
        }

        [Test]
        public void CanLoadExteriorColors()
        {
            var repo = new ExteriorColorRepositoryADO();

            var colors = repo.GetExteriorColors();

            Assert.AreEqual(5, colors.Count);

            Assert.AreEqual(1, colors[0].ExteriorColorId);
            Assert.AreEqual("Black", colors[0].ExteriorColorName);
        }

        [Test]
        public void CanLoadInteriorColors()
        {
            var repo = new InteriorColorRepositoryADO();

            var colors = repo.GetInteriorColors();

            Assert.AreEqual(5, colors.Count);

            Assert.AreEqual(1, colors[0].InteriorColorId);
            Assert.AreEqual("Black", colors[0].InteriorColorName);
        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new MakeRepositoryADO();

            var makes = repo.GetMakes();

            Assert.AreEqual(5, makes.Count);

            Assert.AreEqual(1, makes[0].MakeId);
            Assert.AreEqual("Ford", makes[0].MakeName);
            Assert.AreEqual("jamie@guildcars.com", makes[0].UserEmail);
        }

        [Test]
        public void CanInsertMake()
        {
            var repo = new MakeRepositoryADO();
            Make make = new Make();

            make.MakeName = "Hyundai";
            make.UserEmail = "jamie@guildcars.com";

            repo.Insert(make);

            var allMakes = repo.GetMakes();

            Assert.AreEqual(6, allMakes.Count());

            Assert.AreEqual(6, allMakes[5].MakeId);
            Assert.AreEqual("Hyundai", allMakes[5].MakeName);
            Assert.AreEqual("jamie@guildcars.com", allMakes[5].UserEmail);
        }
            
        [Test]
        public void CanLoadModels()
        {
            var repo = new ModelRepositoryADO();

            var models = repo.GetModels();

            Assert.AreEqual(11, models.Count);

            Assert.AreEqual(1, models[0].ModelId);
            Assert.AreEqual(1, models[0].Make.MakeId);
            Assert.AreEqual("Edge", models[0].ModelName);
            Assert.AreEqual("jamie@guildcars.com", models[0].UserEmail);
        }

        [Test]
        public void CanInsertModel()
        {
            var repo = new ModelRepositoryADO();
            VehicleModel model = new VehicleModel();
            model.Make.MakeId = 3;
            model.ModelName = "Camry";
            model.UserEmail = "jamie@guildcars.com";

            repo.Insert(model);

            var allModels = repo.GetModels();

            Assert.AreEqual(12, allModels.Count());

            Assert.AreEqual(12, allModels[11].ModelId);
            Assert.AreEqual(3, allModels[11].Make.MakeId);
            Assert.AreEqual("Camry", allModels[11].ModelName);
            Assert.AreEqual("jamie@guildcars.com", allModels[11].UserEmail);
        }

        [Test]
        public void CanLoadTransmissionTypes()
        {
            var repo = new TransmissionTypeRepositoryADO();

            var transmissionTypes = repo.GetTransmissionTypes();

            Assert.AreEqual(2, transmissionTypes.Count);

            Assert.AreEqual(1, transmissionTypes[0].TransmissionTypeId);
            Assert.AreEqual("Automatic", transmissionTypes[0].TransmissionTypeName);
        }

        [Test]
        public void CanLoadFeaturedVehicles()
        {
            var repo = new VehicleRepositoryADO();
            var featured = repo.GetFeatured();

            Assert.AreEqual(3, featured.Count);

            Assert.AreEqual("1C4SDJGJ9JC416162", featured[0].VehicleId);
            Assert.AreEqual(69900.00, featured[0].ListedPrice);
            Assert.AreEqual("Dodge", featured[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Durango", featured[0].VehicleModel.ModelName);
            Assert.AreEqual(2018, featured[0].Year);
            Assert.AreEqual("inventory-1C4SDJGJ9JC416162.jpg", featured[0].ImageFileName);
        }

        [Test]
        public void CanLoadVehicleDetails()
        {
            var repo = new VehicleRepositoryADO();
            var vehicleId = "1C4SDJGJ9JC416162";
            var vehicle = repo.GetById(vehicleId);

            Assert.AreEqual("1C4SDJGJ9JC416162", vehicle.VehicleId);
            Assert.AreEqual("Durango", vehicle.VehicleModel.ModelName);
            Assert.AreEqual("Dodge", vehicle.VehicleModel.Make.MakeName);
            Assert.AreEqual("Black", vehicle.ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Black", vehicle.InteriorColor.InteriorColorName);
            Assert.AreEqual("Automatic", vehicle.TransmissionType.TransmissionTypeName);
            Assert.AreEqual("SUV", vehicle.BodyStyle.BodyStyleName);
            Assert.AreEqual(11503, vehicle.Mileage);
            //Assert.AreEqual(69900.00, vehicle.ListedPrice);
            //Assert.AreEqual(69000.00, vehicle.MSRP);
            Assert.AreEqual("inventory-1C4SDJGJ9JC416162.jpg", vehicle.ImageFileName);
            Assert.AreEqual("A really nice looking vehicle", vehicle.Description);

        }

        [Test]
        public void CanInsertVehicle()
        {
            var repo = new VehicleRepositoryADO();
            Vehicle vehicle = new Vehicle();
            vehicle.VehicleId = "aaaaaaaaaaaaaaaaa";
            vehicle.VehicleModel.ModelId = 8;
            vehicle.InteriorColor.InteriorColorId = 3;
            vehicle.ExteriorColor.ExteriorColorId = 4;
            vehicle.TransmissionType.TransmissionTypeId = 1;
            vehicle.BodyStyle.BodyStyleId = 2;
            vehicle.Mileage = 321;
            vehicle.Year = 2021;
            vehicle.ListedPrice = 75500;
            vehicle.MSRP = 77000;
            vehicle.Description = "This vehicle is awesome! Buy it! You know you want to.";
            vehicle.ImageFileName = "inventory-aaaaaaaaaaaaaaaaa.jpg";
            vehicle.IsFeatured = true;
            vehicle.IsUsed = false;
            vehicle.IsSold = false;
            vehicle.UserEmail = "jamie@guildcars.com";

            repo.Insert(vehicle);

            var id = "aaaaaaaaaaaaaaaaa";
            var newVehicle = repo.GetById(id);

            Assert.AreEqual("aaaaaaaaaaaaaaaaa", newVehicle.VehicleId);
            Assert.AreEqual(8, newVehicle.VehicleModel.ModelId);
            Assert.AreEqual(3, newVehicle.InteriorColor.InteriorColorId);
            Assert.AreEqual(4, newVehicle.ExteriorColor.ExteriorColorId);
            Assert.AreEqual(1, newVehicle.TransmissionType.TransmissionTypeId);
            Assert.AreEqual(2, newVehicle.BodyStyle.BodyStyleId);
            Assert.AreEqual(321, newVehicle.Mileage);
            Assert.AreEqual(2021, newVehicle.Year);
            Assert.AreEqual(75500, newVehicle.ListedPrice);
            Assert.AreEqual(77000, newVehicle.MSRP);
            Assert.AreEqual("This vehicle is awesome! Buy it! You know you want to.", newVehicle.Description);
            Assert.AreEqual("inventory-aaaaaaaaaaaaaaaaa.jpg", newVehicle.ImageFileName);
            Assert.AreEqual(true, newVehicle.IsFeatured);
            Assert.AreEqual(false, newVehicle.IsUsed);
            Assert.AreEqual(false, newVehicle.IsSold);
            Assert.AreEqual("jamie@guildcars.com", newVehicle.UserEmail);

        }

        [Test]
        public void CanUpdateVehicle()
        {
            var repo = new VehicleRepositoryADO();
            Vehicle vehicleToAdd = new Vehicle();
            vehicleToAdd.VehicleId = "aaaaaaaaaaaaaaaaa";
            vehicleToAdd.VehicleModel.ModelId = 8;
            vehicleToAdd.InteriorColor.InteriorColorId = 3;
            vehicleToAdd.ExteriorColor.ExteriorColorId = 4;
            vehicleToAdd.TransmissionType.TransmissionTypeId = 1;
            vehicleToAdd.BodyStyle.BodyStyleId = 2;
            vehicleToAdd.Mileage = 321;
            vehicleToAdd.Year = 2021;
            vehicleToAdd.ListedPrice = 75500;
            vehicleToAdd.MSRP = 77000;
            vehicleToAdd.Description = "This vehicle is awesome! Buy it! You know you want to.";
            vehicleToAdd.ImageFileName = "inventory-aaaaaaaaaaaaaaaaa.jpg";
            vehicleToAdd.IsFeatured = true;
            vehicleToAdd.IsUsed = false;
            vehicleToAdd.IsSold = false;
            vehicleToAdd.UserEmail = "jamie@guildcars.com";

            repo.Insert(vehicleToAdd);

            vehicleToAdd.VehicleModel.ModelId = 9;
            vehicleToAdd.InteriorColor.InteriorColorId = 4;
            vehicleToAdd.ExteriorColor.ExteriorColorId = 3;
            vehicleToAdd.TransmissionType.TransmissionTypeId = 2;
            vehicleToAdd.BodyStyle.BodyStyleId = 1;
            vehicleToAdd.Mileage = 325;
            vehicleToAdd.Year = 2021;
            vehicleToAdd.ListedPrice = 500;
            vehicleToAdd.MSRP = 77000;
            vehicleToAdd.Description = "MAJOR PRICE DROP";
            vehicleToAdd.ImageFileName = "inventory-aaaaaaaaaaaaaaaaa.jpg";
            vehicleToAdd.IsFeatured = true;
            vehicleToAdd.IsUsed = false;
            vehicleToAdd.IsSold = false;
            vehicleToAdd.UserEmail = "jamie@guildcars.com";

            repo.Update(vehicleToAdd);

            var id = "aaaaaaaaaaaaaaaaa";
            var updatedVehicle = repo.GetById(id);

            Assert.AreEqual("aaaaaaaaaaaaaaaaa", updatedVehicle.VehicleId);
            Assert.AreEqual(9, updatedVehicle.VehicleModel.ModelId);
            Assert.AreEqual(4, updatedVehicle.InteriorColor.InteriorColorId);
            Assert.AreEqual(3, updatedVehicle.ExteriorColor.ExteriorColorId);
            Assert.AreEqual(2, updatedVehicle.TransmissionType.TransmissionTypeId);
            Assert.AreEqual(1, updatedVehicle.BodyStyle.BodyStyleId);
            Assert.AreEqual(325, updatedVehicle.Mileage);
            Assert.AreEqual(2021, updatedVehicle.Year);
            Assert.AreEqual(500, updatedVehicle.ListedPrice);
            Assert.AreEqual(77000, updatedVehicle.MSRP);
            Assert.AreEqual("MAJOR PRICE DROP", updatedVehicle.Description);
            Assert.AreEqual("inventory-aaaaaaaaaaaaaaaaa.jpg", updatedVehicle.ImageFileName);
            Assert.AreEqual(true, updatedVehicle.IsFeatured);
            Assert.AreEqual(false, updatedVehicle.IsUsed);
            Assert.AreEqual(false, updatedVehicle.IsSold);
            Assert.AreEqual("jamie@guildcars.com", updatedVehicle.UserEmail);
        }

        [Test]
        public void CanDeleteVehicle()
        {
            var repo = new VehicleRepositoryADO();
            Vehicle vehicleToAdd = new Vehicle();
            vehicleToAdd.VehicleId = "aaaaaaaaaaaaaaaaa";
            vehicleToAdd.VehicleModel.ModelId = 8;
            vehicleToAdd.InteriorColor.InteriorColorId = 3;
            vehicleToAdd.ExteriorColor.ExteriorColorId = 4;
            vehicleToAdd.TransmissionType.TransmissionTypeId = 1;
            vehicleToAdd.BodyStyle.BodyStyleId = 2;
            vehicleToAdd.Mileage = 321;
            vehicleToAdd.Year = 2021;
            vehicleToAdd.ListedPrice = 75500;
            vehicleToAdd.MSRP = 77000;
            vehicleToAdd.Description = "This vehicle is awesome! Buy it! You know you want to.";
            vehicleToAdd.ImageFileName = "inventory-aaaaaaaaaaaaaaaaa.jpg";
            vehicleToAdd.IsFeatured = true;
            vehicleToAdd.IsUsed = false;
            vehicleToAdd.IsSold = false;
            vehicleToAdd.UserEmail = "jamie@guildcars.com";

            repo.Insert(vehicleToAdd);
            repo.Delete(vehicleToAdd.VehicleId);


            var deletedVehicle = repo.GetById(vehicleToAdd.VehicleId);

            Assert.IsNull(deletedVehicle.VehicleId);
        }

        [Test]
        public void CanSearchVehicles()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = false;
            parameters.SearchParameter = null;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(2, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByNewORUsed()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = true;
            parameters.SearchParameter = null;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(7, searchResults.Count());

            Assert.AreEqual("1C4SDJGJ9JC416162", searchResults[0].VehicleId);
            Assert.AreEqual(2018, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Durango", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("SUV", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Automatic", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Black", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Black", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(11503, searchResults[0].Mileage);
            Assert.AreEqual(69900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(72000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-1C4SDJGJ9JC416162.jpg", searchResults[0].ImageFileName);
        }


        [Test]
        public void CanSearchVehiclesByMake()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.SearchParameter = "Dod";
            parameters.IsUsed = false;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(1, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByModel()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.SearchParameter = "Cha";
            parameters.IsUsed = false;
            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(1, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByYear()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.SearchParameter = "21";
            parameters.IsUsed = false;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(2, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByMinPrice()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = false;
            parameters.MinPrice = 36000;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(1, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByMaxPrice()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = false;
            parameters.MaxPrice = 36000;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(1, searchResults.Count());

            Assert.AreEqual("1FMEE5BH1MLA96289", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Toyota", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("ForeRunner", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("SUV", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Automatic", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Guild Gray", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Red", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(100, searchResults[0].Mileage);
            Assert.AreEqual(35900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(38000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-1FMEE5BH1MLA96289.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByPriceRange()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = false;
            parameters.MinPrice = 35000;
            parameters.MaxPrice = 37000;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(2, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByMinYear()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = false;
            parameters.MinYear = 2021;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(2, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByMaxYear()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = false;
            parameters.MaxYear = 2021;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(2, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByYearRange()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = false;
            parameters.MaxYear = 2021;
            parameters.MinYear = 2017;

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(2, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanSearchVehiclesByAllParameters()
        {
            var repo = new VehicleRepositoryADO();
            var parameters = new VehicleSearchParameters();
            parameters.IsUsed = false;
            parameters.MaxYear = 2021;
            parameters.MinPrice = 36000;
            parameters.SearchParameter = "Chal";

            var searchResults = repo.VehicleSearch(parameters);

            Assert.AreEqual(1, searchResults.Count());

            Assert.AreEqual("2C3CDZGG7HH628383", searchResults[0].VehicleId);
            Assert.AreEqual(2021, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Challenger", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("Car", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Manual", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Silver", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Guild Gray", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(96, searchResults[0].Mileage);
            Assert.AreEqual(36900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(40000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-2C3CDZGG7HH628383.jpg", searchResults[0].ImageFileName);
        }
        [Test]
        public void CanGetAllInventory()
        {
            var repo = new VehicleRepositoryADO();
            var searchResults = repo.GetAllVehicles();

            Assert.AreEqual(9, searchResults.Count());

            Assert.AreEqual("1C4SDJGJ9JC416162", searchResults[0].VehicleId);
            Assert.AreEqual(2018, searchResults[0].Year);
            Assert.AreEqual("Dodge", searchResults[0].VehicleModel.Make.MakeName);
            Assert.AreEqual("Durango", searchResults[0].VehicleModel.ModelName);
            Assert.AreEqual("SUV", searchResults[0].BodyStyle.BodyStyleName);
            Assert.AreEqual("Automatic", searchResults[0].TransmissionType.TransmissionTypeName);
            Assert.AreEqual("Black", searchResults[0].ExteriorColor.ExteriorColorName);
            Assert.AreEqual("Black", searchResults[0].InteriorColor.InteriorColorName);
            Assert.AreEqual(11503, searchResults[0].Mileage);
            Assert.AreEqual(69900.00, searchResults[0].ListedPrice);
            Assert.AreEqual(72000.00, searchResults[0].MSRP);
            Assert.AreEqual("inventory-1C4SDJGJ9JC416162.jpg", searchResults[0].ImageFileName);
        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new StateRepositoryADO();

            var states = repo.GetStates();

            Assert.AreEqual(5, states.Count);

            Assert.AreEqual("IL", states[0].StateAbbreviation);
            Assert.AreEqual("Illinois", states[0].StateName);
        }

        //Specials Tests------------------------------------------------------------------

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialsRepositoryADO();

            var specials = repo.GetAll();

            Assert.AreEqual(2, specials.Count);

            Assert.AreEqual(1, specials[0].SpecialId);
            Assert.AreEqual("Free Tires For Life!", specials[0].SpecialName);
            Assert.AreEqual("freeTires.jpg", specials[0].SpecialImageFilename);
            Assert.AreEqual("You can earn free tires for the life of your vehicle if you purchase today.", specials[0].SpecialDescription);
            Assert.AreEqual("jamie@guildcars.com", specials[0].UserEmail);
        }

        [Test]
        public void CanInsertSpecial()
        {
            var repo = new SpecialsRepositoryADO();
            Special special = new Special();
            special.SpecialImageFilename = "freeTires.jpg";
            special.SpecialName = "VAN SALE!!";
            special.SpecialDescription = "We are having a blowout sale on all in-stock VANS!!";
            special.UserEmail = "jamie@guildcars.com";

            repo.Insert(special);

            var allSpecials = repo.GetAll();

            Assert.AreEqual(3, allSpecials.Count());
            Assert.AreEqual("VAN SALE!!", allSpecials[2].SpecialName);
            Assert.AreEqual("We are having a blowout sale on all in-stock VANS!!", allSpecials[2].SpecialDescription);
            Assert.AreEqual("jamie@guildcars.com", allSpecials[2].UserEmail);
        }

        [Test]
        public void CanDeleteSpecial()
        {
            var repo = new SpecialsRepositoryADO();
            Special specialToAdd = new Special();

            specialToAdd.SpecialName = "Free Oil Change";
            specialToAdd.SpecialDescription = "Free Oil changes for the life of your warranty with the purchase of a new vehicle!";
            specialToAdd.UserEmail = "jamie@guildcars.com";

            repo.Insert(specialToAdd);

            var allSpecials = repo.GetAll();

            Assert.AreEqual(3, allSpecials.Count);
            Assert.AreEqual("Free Oil Change", allSpecials[2].SpecialName);

            repo.Delete(specialToAdd.SpecialId);

            allSpecials = repo.GetAll();

            Assert.AreEqual(2, allSpecials.Count);
        }


        //Contact Request Tests-----------------------------------------------------------------
        [Test]
        public void CanLoadContactRequests()
        {
            var repo = new ContactRequestRepositoryADO();

            var contactRequests = repo.GetAll();

            Assert.AreEqual(2, contactRequests.Count);

            Assert.AreEqual(1, contactRequests[0].ContactRequestId);
            Assert.AreEqual("1FMEE5BH1MLA96289", contactRequests[0].VehicleId);
            Assert.AreEqual("Bob Bilby", contactRequests[0].ContactName);
            Assert.AreEqual(string.Empty, contactRequests[0].ContactEmail);
            Assert.AreEqual("502-555-5555", contactRequests[0].ContactPhone);
            Assert.AreEqual("I would like to buy this car!", contactRequests[0].ContactMessage);
        }

        [Test]
        public void CanInsertContactRequests()
        {
            var repo = new ContactRequestRepositoryADO();

            ContactRequest contactRequest = new ContactRequest();
            contactRequest.VehicleId = "1C4SDJGJ9JC416162";
            contactRequest.ContactName = "Jamie";
            contactRequest.ContactEmail = "request@request.com";
            contactRequest.ContactPhone = "502-222-2222";
            contactRequest.ContactMessage = "I like this car";

            repo.Insert(contactRequest);

            var contactRequests = repo.GetAll();

            Assert.AreEqual(3, contactRequests.Count());
            Assert.AreEqual(3, contactRequests[2].ContactRequestId);
            Assert.AreEqual("1C4SDJGJ9JC416162", contactRequests[2].VehicleId);
            Assert.AreEqual("Jamie", contactRequests[2].ContactName);
            Assert.AreEqual("request@request.com", contactRequests[2].ContactEmail);
            Assert.AreEqual("502-222-2222", contactRequests[2].ContactPhone);
            Assert.AreEqual("I like this car", contactRequests[2].ContactMessage);
        }

        //Customer Test-----------------------------------------------------------------------------------
        [Test]
        public void CanInsertCustomer()
        {
            var repo = new CustomerRepositoryADO();
            Customer customer = new Customer();

            customer.CustomerName = "Burt McHandsome";
            customer.CustomerPhone = "502-555-5522";
            customer.CustomerAddress1 = "44 Wallaby Way";
            customer.City = "Lexington";
            customer.Zip = "40000";
            customer.State.StateAbbreviation = "KY";
            customer.CustomerEmail = "Burt@handsomeness.com";

            repo.Insert(customer);

            Assert.AreEqual(2, customer.CustomerId);
        }

        //Purchase Tests--------------------------------------------------------------------------------------
        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new PurchaseTypeRepositoryADO();

            var purchaseTypes = repo.GetAllPurchaseTypes();

            Assert.AreEqual(3, purchaseTypes.Count);

            Assert.AreEqual(1, purchaseTypes[0].PurchaseTypeId);
            Assert.AreEqual("Bank Finance", purchaseTypes[0].PurchaseTypeName);
        }

        [Test]
        public void CanLoadSalesReport()
        {
            var repo = new PurchaseRepositoryADO();
            var parameters = new SalesReportSearchParameters();
            var salesReport = repo.GetSalesReport(parameters);

            Assert.AreEqual(1, salesReport.Count);

        }

        //Customer Test-----------------------------------------------------------------------------------
        [Test]
        public void CanInsertPurchase()
        {
            var repo = new PurchaseRepositoryADO();
            Purchase purchase = new Purchase();

            purchase.PurchaseType.PurchaseTypeId = 1;
            purchase.Vehicle.VehicleId = "1FMEE5BH1MLA96289";
            purchase.Customer.CustomerId = 1;
            purchase.PurchasePrice = 34800;
            purchase.UserEmail = "jamie@guildcars.com";

            repo.Insert(purchase);

            Assert.AreEqual(2, purchase.PurchaseId);
        }


    }
}
