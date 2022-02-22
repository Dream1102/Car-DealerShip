using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories.QA
{
    public class PurchaseRepositoryQA : IPurchaseRepository
    {
        static DateTime date = new DateTime(2021, 11, 29);
        List<Purchase> _purchases = new List<Purchase>();

        public PurchaseRepositoryQA()
        {
            SeedPurchases();
        }

        public void SeedPurchases()
        {
            Purchase purchase = new Purchase();
            purchase.PurchaseId = 1;
            purchase.PurchaseType.PurchaseTypeId = 2;
            purchase.PurchaseType.PurchaseTypeName = "Cash";
            purchase.Customer.CustomerId = 1;
            purchase.Customer.State.StateAbbreviation = "KY";
            purchase.Customer.State.StateName = "Kentucky";
            purchase.Customer.CustomerName = "Telemachus";
            purchase.Customer.CustomerPhone = "502-555-5555";
            purchase.Customer.CustomerAddress1 = "523 Colonel Hill";
            purchase.Customer.City = "Louisville";
            purchase.Customer.Zip = "40242";
            purchase.Customer.CustomerEmail = "tel@machus.com";
            purchase.Vehicle.VehicleId = "1FMHK7F83BGA02897";
            purchase.Vehicle.VehicleModel.ModelId = 2;
            purchase.Vehicle.VehicleModel.ModelName = "Explorer";
            purchase.Vehicle.VehicleModel.Make.MakeId = 1;
            purchase.Vehicle.VehicleModel.Make.MakeName = "Ford";
            purchase.Vehicle.InteriorColor.InteriorColorId = 2;
            purchase.Vehicle.InteriorColor.InteriorColorName = "Guild Red";
            purchase.Vehicle.ExteriorColor.ExteriorColorId = 3;
            purchase.Vehicle.ExteriorColor.ExteriorColorName = "Guild Gray";
            purchase.Vehicle.TransmissionType.TransmissionTypeId = 1;
            purchase.Vehicle.TransmissionType.TransmissionTypeName = "Automatic";
            purchase.Vehicle.BodyStyle.BodyStyleId = 2;
            purchase.Vehicle.BodyStyle.BodyStyleName = "SUV";
            purchase.Vehicle.Mileage = 50;
            purchase.Vehicle.Year = 2019;
            purchase.Vehicle.DateAdded = date;
            purchase.Vehicle.ListedPrice = 55000;
            purchase.Vehicle.MSRP = 57000;
            purchase.Vehicle.Description = "A very new......Explorer";
            purchase.Vehicle.ImageFileName = "inventory-1FMHK7F83BGA02897";
            purchase.Vehicle.IsFeatured = false;
            purchase.Vehicle.IsUsed = false;
            purchase.Vehicle.IsSold = true;
           
            purchase.PurchaseDate = DateTime.Now;
            purchase.PurchasePrice = 10000;
            purchase.UserEmail = "jamie@guildcars.com";

            InsertPurchase(purchase);
        }

        public void InsertPurchase(Purchase purchase)
        {
            _purchases.Add(purchase);
        }

        public List<Purchase> GetAllPurchases()
        {
            List<Purchase> purchases = new List<Purchase>();

            foreach (var purchase in _purchases)
            {
                purchases.Add(purchase);
            }

            return purchases;
        }

        public List<SalesReport> GetSalesReport(SalesReportSearchParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
