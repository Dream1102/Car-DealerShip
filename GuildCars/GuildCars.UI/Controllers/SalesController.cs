using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class SalesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = userMgr.FindByName(User.Identity.Name);
                ViewBag.UserEmail = user.Email;
            }
            var model = new AllInventoryViewModel();
            model.Years = VehicleRepositoryFactory.GetRepository().GetYears();
            model.Prices = VehicleRepositoryFactory.GetRepository().GetListPrices();

            return View(model);
        }

        [HttpGet]
        public ActionResult Purchase(string id)
        {
            var model = new PurchaseVehicleViewModel(); 
            var vehicleRepo = VehicleRepositoryFactory.GetRepository();

            model.Vehicle = vehicleRepo.GetById(id);
            model.PurchaseTypes = GetPurchaseTypesSelectList();
            model.States = GetStatesSelectList();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Purchase(PurchaseVehicleViewModel purchase)
        {
            purchase.States = GetStatesSelectList();
            purchase.PurchaseTypes = GetPurchaseTypesSelectList();
            
            if (ModelState.IsValid)
            {
                var customerId = GetCustomerId(purchase);
                purchase.Customer.CustomerId = customerId;
                var customerExists = CheckIfCustomerExists(purchase);

                if (customerExists == true)
                {
                    SavePurchase(purchase);
                    EditVehicle(purchase);
                }
                else
                {
                    InsertCustomer(purchase);
                    SavePurchase(purchase);
                    EditVehicle(purchase);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(purchase);
            }
        }

        [HttpGet]
        public ActionResult Makes()
        {
            var model = new MakesViewModel();
            model.Makes = MakeRepositoryFactory.GetRepository().GetMakes();
            model.Make = new Make();

            return View(model);
        }

        [HttpPost]
        public ActionResult Makes(MakesViewModel newMake)
        {

            if (ModelState.IsValid)
            {
                var repo = MakeRepositoryFactory.GetRepository();
                var allMakes = repo.GetMakes();
                foreach(var make in allMakes)
                {
                    if(make.MakeName == newMake.Make.MakeName)
                    {
                        break;
                    }
                }
                newMake.Make.DateMakeCreated = DateTime.Now;
                newMake.Make.MakeId = GetMakeId(newMake);
                newMake.Make.UserEmail = User.Identity.GetUserName();
                repo.Insert(newMake.Make);
            }

            var model = new MakesViewModel();
            model.Makes = MakeRepositoryFactory.GetRepository().GetMakes();
            model.Make = new Make();

            return View(model);
        }

        [HttpGet]
        public ActionResult Models()
        {
            var makesRepo = MakeRepositoryFactory.GetRepository();
            var model = new ModelsViewModel();
            model.Models = ModelRepositoryFactory.GetRepository().GetModels();
            model.Makes = makesRepo.GetMakes();
            model.VehicleModel = new VehicleModel();
            model.MakesSelectList = new SelectList(makesRepo.GetMakes(), "MakeId", "MakeName");

            return View(model);
        }

        [HttpPost]
        public ActionResult Models(ModelsViewModel newModel)
        {
            if (ModelState.IsValid)
            {
                var repo = ModelRepositoryFactory.GetRepository();
                newModel.VehicleModel.DateModelCreated = DateTime.Now;
                newModel.VehicleModel.UserEmail = User.Identity.GetUserName();
                newModel.VehicleModel.ModelId = GetModelId(newModel);

                repo.Insert(newModel.VehicleModel);
            }

            var makesRepo = MakeRepositoryFactory.GetRepository();
            var model = new ModelsViewModel();
            model.Models = ModelRepositoryFactory.GetRepository().GetModels();
            model.Makes = makesRepo.GetMakes();
            model.VehicleModel = new VehicleModel();
            model.MakesSelectList = new SelectList(makesRepo.GetMakes(), "MakeId", "MakeName");

            return View(model);
        }

        //private Helper methods

        private string GetStates(PurchaseVehicleViewModel purchase)
        {
            var statesRepo = StateRepositoryFactory.GetRepository();
            var states = statesRepo.GetStates();
            
            purchase.Customer.State.StateName = Convert.ToString(from state in states
                             where state.StateAbbreviation == purchase.Customer.State.StateAbbreviation
                             select state.StateName);

            return purchase.Customer.State.StateName;
        }

        private bool CheckIfCustomerExists(PurchaseVehicleViewModel purchase)
        {
            var repo = CustomerRepositoryFactory.GetRepository();
            var allCustomers = repo.GetCustomers();

            bool customerExists = false;

            foreach (var customer in allCustomers)
            {
                if (customer.CustomerName == purchase.Customer.CustomerName && customer.CustomerAddress1 == purchase.Customer.CustomerAddress1 && (customer.CustomerPhone == purchase.Customer.CustomerPhone || customer.CustomerEmail == purchase.Customer.CustomerEmail))
                {
                    customerExists = true;
                }
            }
            return customerExists;
        }

        private void InsertCustomer(PurchaseVehicleViewModel purchase)
        {
            var repo = CustomerRepositoryFactory.GetRepository();
            var customerToSave = new Customer();

            customerToSave.CustomerId = purchase.Customer.CustomerId;
            customerToSave.State.StateAbbreviation = purchase.Customer.State.StateAbbreviation;
            customerToSave.State.StateName = GetStates(purchase);
            customerToSave.CustomerName = purchase.Customer.CustomerName;
            customerToSave.CustomerPhone = purchase.Customer.CustomerPhone;
            customerToSave.CustomerAddress1 = purchase.Customer.CustomerAddress1;
            customerToSave.CustomerAddress2 = purchase.Customer.CustomerAddress2;
            customerToSave.City = purchase.Customer.City;
            customerToSave.Zip = purchase.Customer.Zip;
            customerToSave.CustomerEmail = purchase.Customer.CustomerEmail;

            repo.Insert(customerToSave);
        }

        private void EditVehicle(PurchaseVehicleViewModel purchase)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var vehicleToEdit = new Vehicle();

            vehicleToEdit.VehicleId = purchase.Vehicle.VehicleId;
            vehicleToEdit.VehicleModel.ModelId = purchase.Vehicle.VehicleModel.ModelId;
            vehicleToEdit.VehicleModel.Make.MakeName = purchase.Vehicle.VehicleModel.Make.MakeName;
            vehicleToEdit.VehicleModel.Make.MakeId = purchase.Vehicle.VehicleModel.Make.MakeId;
            vehicleToEdit.InteriorColor.InteriorColorId = purchase.Vehicle.InteriorColor.InteriorColorId;
            vehicleToEdit.InteriorColor.InteriorColorName = purchase.Vehicle.InteriorColor.InteriorColorName;
            vehicleToEdit.ExteriorColor.ExteriorColorId = purchase.Vehicle.ExteriorColor.ExteriorColorId;
            vehicleToEdit.ExteriorColor.ExteriorColorName = purchase.Vehicle.ExteriorColor.ExteriorColorName;
            vehicleToEdit.TransmissionType.TransmissionTypeId = purchase.Vehicle.TransmissionType.TransmissionTypeId;
            vehicleToEdit.TransmissionType.TransmissionTypeName = purchase.Vehicle.TransmissionType.TransmissionTypeName;
            vehicleToEdit.BodyStyle.BodyStyleId = purchase.Vehicle.BodyStyle.BodyStyleId;
            vehicleToEdit.BodyStyle.BodyStyleName = purchase.Vehicle.BodyStyle.BodyStyleName;
            vehicleToEdit.Mileage = purchase.Vehicle.Mileage;
            vehicleToEdit.Year = purchase.Vehicle.Year;
            vehicleToEdit.DateAdded = purchase.Vehicle.DateAdded;
            vehicleToEdit.ListedPrice = purchase.Vehicle.ListedPrice;
            vehicleToEdit.MSRP = purchase.Vehicle.MSRP;
            vehicleToEdit.Description = purchase.Vehicle.Description;
            vehicleToEdit.ImageFileName = purchase.Vehicle.ImageFileName;
            vehicleToEdit.IsFeatured = false;
            vehicleToEdit.IsUsed = purchase.Vehicle.IsUsed;
            vehicleToEdit.IsSold = true;
            vehicleToEdit.UserEmail = purchase.Vehicle.UserEmail;

            repo.Update(vehicleToEdit);
        }

        private void SavePurchase(PurchaseVehicleViewModel purchase)
        {
            var repo = PurchaseRepositoryFactory.GetRepository();
            var statesRepo = StateRepositoryFactory.GetRepository();
            var purchaseTypeRepo = PurchaseTypeRepositoryFactory.GetRepository();
            
            var purchaseToSave = new Purchase();
            var states = statesRepo.GetStates();
            var purchaseTypes = purchaseTypeRepo.GetAllPurchaseTypes();

            purchaseToSave.PurchaseId = GetPurchaseId(purchase);
            purchaseToSave.Customer.CustomerId = purchase.Customer.CustomerId;
            purchaseToSave.Customer.State.StateAbbreviation = purchase.Customer.State.StateAbbreviation;
            purchaseToSave.Customer.State.StateName = GetStates(purchase);
            purchaseToSave.Customer.CustomerName = purchase.Customer.CustomerName;
            purchaseToSave.Customer.CustomerPhone = purchase.Customer.CustomerPhone;
            purchaseToSave.Customer.CustomerAddress1 = purchase.Customer.CustomerAddress1;
            purchaseToSave.Customer.CustomerAddress2 = purchase.Customer.CustomerAddress2;
            purchaseToSave.Customer.City = purchase.Customer.City;
            purchaseToSave.Customer.Zip = purchase.Customer.Zip;
            purchaseToSave.Customer.CustomerEmail = purchase.Customer.CustomerEmail;
            purchaseToSave.Vehicle.VehicleId = purchase.Vehicle.VehicleId;
            purchaseToSave.Vehicle.VehicleModel.ModelId = purchase.Vehicle.VehicleModel.ModelId;
            purchaseToSave.Vehicle.VehicleModel.Make.MakeName = purchase.Vehicle.VehicleModel.Make.MakeName;
            purchaseToSave.Vehicle.VehicleModel.Make.MakeId = purchase.Vehicle.VehicleModel.Make.MakeId;
            purchaseToSave.Vehicle.InteriorColor.InteriorColorId = purchase.Vehicle.InteriorColor.InteriorColorId;
            purchaseToSave.Vehicle.InteriorColor.InteriorColorName = purchase.Vehicle.InteriorColor.InteriorColorName;
            purchaseToSave.Vehicle.ExteriorColor.ExteriorColorId = purchase.Vehicle.ExteriorColor.ExteriorColorId;
            purchaseToSave.Vehicle.ExteriorColor.ExteriorColorName = purchase.Vehicle.ExteriorColor.ExteriorColorName;
            purchaseToSave.Vehicle.TransmissionType.TransmissionTypeId = purchase.Vehicle.TransmissionType.TransmissionTypeId;
            purchaseToSave.Vehicle.TransmissionType.TransmissionTypeName = purchase.Vehicle.TransmissionType.TransmissionTypeName;
            purchaseToSave.Vehicle.BodyStyle.BodyStyleId = purchase.Vehicle.BodyStyle.BodyStyleId;
            purchaseToSave.Vehicle.BodyStyle.BodyStyleName = purchase.Vehicle.BodyStyle.BodyStyleName;
            purchaseToSave.Vehicle.Mileage = purchase.Vehicle.Mileage;
            purchaseToSave.Vehicle.Year = purchase.Vehicle.Year;
            purchaseToSave.Vehicle.DateAdded = purchase.Vehicle.DateAdded;
            purchaseToSave.Vehicle.ListedPrice = purchase.Vehicle.ListedPrice;
            purchaseToSave.Vehicle.MSRP = purchase.Vehicle.MSRP;
            purchaseToSave.Vehicle.Description = purchase.Vehicle.Description;
            purchaseToSave.Vehicle.ImageFileName = purchase.Vehicle.ImageFileName;
            purchaseToSave.Vehicle.IsFeatured = false;
            purchaseToSave.Vehicle.IsUsed = purchase.Vehicle.IsUsed;
            purchaseToSave.Vehicle.IsSold = true;
            purchaseToSave.UserEmail = User.Identity.GetUserName();
            purchaseToSave.PurchaseType.PurchaseTypeId = Convert.ToInt32(purchase.PurchaseType);
            purchaseToSave.PurchaseType.PurchaseTypeName = (from pt in purchaseTypes
                                                                          where pt.PurchaseTypeName == purchase.PurchaseType
                                                                          select pt.PurchaseTypeId).ToString();
            purchaseToSave.PurchasePrice = purchase.PurchasePrice;
            purchaseToSave.PurchaseDate = DateTime.Now;

            repo.Insert(purchaseToSave);
        }

        private int GetCustomerId(PurchaseVehicleViewModel purchase)
        {
            var customerRepo = CustomerRepositoryFactory.GetRepository();
            var allCustomers = customerRepo.GetCustomers();
            foreach (var customer in allCustomers)
            {
                if (customer.CustomerName == purchase.Customer.CustomerName && customer.CustomerAddress1 == purchase.Customer.CustomerAddress1 && (customer.CustomerPhone == purchase.Customer.CustomerPhone || customer.CustomerEmail == purchase.Customer.CustomerEmail))
                {
                    purchase.Customer.CustomerId = customer.CustomerId;
                }
                else
                {
                    purchase.Customer.CustomerId = allCustomers.Select(c => c.CustomerId).LastOrDefault() + 1;              
                } 
            }
            return purchase.Customer.CustomerId;
        }

        private int GetMakeId(MakesViewModel newMake)
        {
            var makesRepo = MakeRepositoryFactory.GetRepository();

            var makeId = makesRepo.GetMakes().Select(p => p.MakeId).LastOrDefault() + 1;

            return makeId;
        }
        private int GetModelId(ModelsViewModel newModel)
        {
            var modelsRepo = ModelRepositoryFactory.GetRepository();

            var modelId = modelsRepo.GetModels().Select(p => p.ModelId).LastOrDefault() + 1;

            return modelId;
        }

        private int GetPurchaseId(PurchaseVehicleViewModel purchase)
        {
            var purchaseRepo = PurchaseRepositoryFactory.GetRepository();

            var purchaseId = purchaseRepo.GetPurchases().Select(p => p.PurchaseId).LastOrDefault() + 1;

            return purchaseId;
        }

        private List<SelectListItem> GetStatesSelectList()
        {
            var repo = StateRepositoryFactory.GetRepository();
            var states = repo.GetStates();


            List<SelectListItem> items = states.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.StateAbbreviation,
                    Value = p.StateAbbreviation,
                    Selected = false
                };
            });

            return items;
        }

        private List<SelectListItem> GetPurchaseTypesSelectList()
        {
            var repo = PurchaseTypeRepositoryFactory.GetRepository();
            var purchaseTypes = repo.GetAllPurchaseTypes();


            List<SelectListItem> items = purchaseTypes.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.PurchaseTypeName.ToString(),
                    Value = p.PurchaseTypeId.ToString(),
                    Selected = false
                };
            });

            return items;
        }
    }
}