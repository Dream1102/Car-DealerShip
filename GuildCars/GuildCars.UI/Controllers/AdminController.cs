using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Vehicles()
        {
            if (Request.IsAuthenticated)
            {
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var user = userMgr.FindByName(User.Identity.Name);
                ViewBag.UserEmail = user.Email;
            }

            var model = new AllInventoryViewModel();
            model.Years = GetYears();
            model.Prices = GetListPrices();

            return View(model);
        }

        [HttpGet]
        public ActionResult Specials()
        {
                var repo = SpecialsRepositoryFactory.GetRepository();
                var model = new SpecialsViewModel();
                model.Special = new Special();
                model.SpecialsList = repo.GetAll();

                return View(model);
        }

        [HttpPost]
        public ActionResult Specials(SpecialsViewModel newSpecial)
        {
            if(ModelState.IsValid)
            {
                var specialsRepo = SpecialsRepositoryFactory.GetRepository();
                newSpecial.Special.DateSpecialCreated = DateTime.Now;
                newSpecial.Special.UserEmail = User.Identity.GetUserName();
                newSpecial.Special.SpecialId = GetSpecialId(newSpecial);

                if (newSpecial.ImageUpload != null && newSpecial.ImageUpload.ContentLength > 0)
                {
                    var savePath = Server.MapPath("~/Images");
                    string extension = Path.GetExtension(newSpecial.ImageUpload.FileName);
                    string fileName = "special-" + newSpecial.Special.SpecialName;
                    var filePath = Path.Combine(savePath, fileName + extension);

                    int counter = 1;
                    while (System.IO.File.Exists(filePath))
                    {
                        filePath = Path.Combine(savePath, fileName + counter.ToString() + extension);
                        counter++;
                    }

                    newSpecial.ImageUpload.SaveAs(filePath);
                    newSpecial.Special.SpecialImageFilename = Path.GetFileName(filePath);
                }

                specialsRepo.Insert(newSpecial.Special);

                return RedirectToAction("Specials");
            }
            else
            {
                var repo = SpecialsRepositoryFactory.GetRepository();
                var model = new SpecialsViewModel();
                model.Special = new Special();
                model.SpecialsList = repo.GetAll();

                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteSpecial(int id)
        {
            var repo = SpecialsRepositoryFactory.GetRepository();
            repo.Delete(id);

            var model = new SpecialsViewModel();
            model.Special = new Special();
            model.SpecialsList = repo.GetAll();

            return RedirectToAction("Specials");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddVehicle()
        {
            var makesRepo = MakeRepositoryFactory.GetRepository();
            var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
            var transmissionTypesRepo = TransmissionTypeRepositoryFactory.GetRepository();
            var exteriorColorsRepo = ExteriorColorRepositoryFactory.GetRepository();
            var interiorColorsRepo = InteriorColorRepositoryFactory.GetRepository();

            var model = new AddVehicleViewModel();

            model.MakeList = new SelectList(makesRepo.GetMakes(), "MakeId", "MakeName");
            model.BodyStyleList = new SelectList(bodyStylesRepo.GetBodyStyles(), "BodyStyleId", "BodyStyleName");
            model.TransmissionTypeList = new SelectList(transmissionTypesRepo.GetTransmissionTypes(), "TransmissionTypeId", "TransmissionTypeName");
            model.ExteriorColorList = new SelectList(exteriorColorsRepo.GetExteriorColors(), "ExteriorColorId", "ExteriorColorName");
            model.InteriorColorList = new SelectList(interiorColorsRepo.GetInteriorColors(), "InteriorColorId", "InteriorColorName");
            model.IsUsed = new List<SelectListItem>
            {
                new SelectListItem {Text = "New", Value = false.ToString()},
                new SelectListItem {Text = "Used", Value = true.ToString()}
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddVehicle(AddVehicleViewModel newVehicle)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepositoryFactory.GetRepository();
                if(newVehicle.ImageUpload !=null && newVehicle.ImageUpload.ContentLength > 0)
                {
                    var savePath = Server.MapPath("~/Images");
                    string extension = Path.GetExtension(newVehicle.ImageUpload.FileName);
                    string fileName = "inventory-" + newVehicle.Vehicle.VehicleId;
                    var filePath = Path.Combine(savePath, fileName + extension);

                    newVehicle.ImageUpload.SaveAs(filePath);
                    newVehicle.Vehicle.ImageFileName = Path.GetFileName(filePath);
                }
                newVehicle.Vehicle.DateAdded = DateTime.Now;
                newVehicle.Vehicle.IsFeatured = false;
                newVehicle.Vehicle.IsSold = false;
                newVehicle.Vehicle.UserEmail = User.Identity.GetUserName();
                
                repo.Insert(newVehicle.Vehicle);

                return RedirectToAction("EditVehicle", new { id = newVehicle.Vehicle.VehicleId });
            }
            else
            {
                var makesRepo = MakeRepositoryFactory.GetRepository();
                var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
                var transmissionTypesRepo = TransmissionTypeRepositoryFactory.GetRepository();
                var exteriorColorsRepo = ExteriorColorRepositoryFactory.GetRepository();
                var interiorColorsRepo = InteriorColorRepositoryFactory.GetRepository();

                newVehicle.MakeList = new SelectList(makesRepo.GetMakes(), "MakeId", "MakeName");
                newVehicle.BodyStyleList = new SelectList(bodyStylesRepo.GetBodyStyles(), "BodyStyleId", "BodyStyleName");
                newVehicle.TransmissionTypeList = new SelectList(transmissionTypesRepo.GetTransmissionTypes(), "TransmissionTypeId", "TransmissionTypeName");
                newVehicle.ExteriorColorList = new SelectList(exteriorColorsRepo.GetExteriorColors(), "ExteriorColorId", "ExteriorColorName");
                newVehicle.InteriorColorList = new SelectList(interiorColorsRepo.GetInteriorColors(), "InteriorColorId", "InteriorColorName");
                newVehicle.IsUsed = new List<SelectListItem>
                {
                    new SelectListItem {Text = "New", Value = false.ToString()},
                    new SelectListItem {Text = "Used", Value = true.ToString()}
                };
                return View(newVehicle);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditVehicle()
        {
            var makesRepo = MakeRepositoryFactory.GetRepository();
            var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
            var transmissionTypesRepo = TransmissionTypeRepositoryFactory.GetRepository();
            var exteriorColorsRepo = ExteriorColorRepositoryFactory.GetRepository();
            var interiorColorsRepo = InteriorColorRepositoryFactory.GetRepository();
            var vehicleRepo = VehicleRepositoryFactory.GetRepository();

            var model = new EditVehicleViewModel();

            var url = Request.RawUrl;
            var vehicleId = url.Substring(19);

            model.Vehicle = vehicleRepo.GetById(vehicleId);

            model.MakeList = new SelectList(makesRepo.GetMakes(), "MakeId", "MakeName");
            model.BodyStyleList = new SelectList(bodyStylesRepo.GetBodyStyles(), "BodyStyleId", "BodyStyleName");
            model.TransmissionTypeList = new SelectList(transmissionTypesRepo.GetTransmissionTypes(), "TransmissionTypeId", "TransmissionTypeName");
            model.ExteriorColorList = new SelectList(exteriorColorsRepo.GetExteriorColors(), "ExteriorColorId", "ExteriorColorName");
            model.InteriorColorList = new SelectList(interiorColorsRepo.GetInteriorColors(), "InteriorColorId", "InteriorColorName");
            model.IsUsed = new List<SelectListItem>
            {
                new SelectListItem {Text = "New", Value = false.ToString()},
                new SelectListItem {Text = "Used", Value = true.ToString()}
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditVehicle(EditVehicleViewModel updatedVehicle)
        {
            if (ModelState.IsValid)
            {
                var repo = VehicleRepositoryFactory.GetRepository();

                if (updatedVehicle.ImageUpload != null && updatedVehicle.ImageUpload.ContentLength > 0)
                {
                    var savePath = Server.MapPath("~/Images");
                    string extension = Path.GetExtension(updatedVehicle.ImageUpload.FileName);
                    string fileName = "inventory-" + updatedVehicle.Vehicle.VehicleId;
                    var filePath = Path.Combine(savePath, fileName + extension);

                    updatedVehicle.ImageUpload.SaveAs(filePath);
                    updatedVehicle.Vehicle.ImageFileName = Path.GetFileName(filePath);
                }
                else
                {
                    updatedVehicle.Vehicle.ImageFileName = repo.GetById(updatedVehicle.Vehicle.VehicleId).ImageFileName;
                }
                updatedVehicle.Vehicle.DateAdded = DateTime.Now;

                updatedVehicle.Vehicle.IsSold = false;
                updatedVehicle.Vehicle.UserEmail = User.Identity.GetUserName();

                repo.Update(updatedVehicle.Vehicle);

                return RedirectToAction("Vehicles");
            }
            else
            {
                var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                var makesRepo = MakeRepositoryFactory.GetRepository();
                var bodyStylesRepo = BodyStyleRepositoryFactory.GetRepository();
                var transmissionTypesRepo = TransmissionTypeRepositoryFactory.GetRepository();
                var exteriorColorsRepo = ExteriorColorRepositoryFactory.GetRepository();
                var interiorColorsRepo = InteriorColorRepositoryFactory.GetRepository();

                var url = Request.RawUrl;
                var vehicleId = url.Substring(19);
                updatedVehicle.Vehicle = vehicleRepo.GetById(vehicleId);

                updatedVehicle.MakeList = new SelectList(makesRepo.GetMakes(), "MakeId", "MakeName");
                updatedVehicle.BodyStyleList = new SelectList(bodyStylesRepo.GetBodyStyles(), "BodyStyleId", "BodyStyleName");
                updatedVehicle.TransmissionTypeList = new SelectList(transmissionTypesRepo.GetTransmissionTypes(), "TransmissionTypeId", "TransmissionTypeName");
                updatedVehicle.ExteriorColorList = new SelectList(exteriorColorsRepo.GetExteriorColors(), "ExteriorColorId", "ExteriorColorName");
                updatedVehicle.InteriorColorList = new SelectList(interiorColorsRepo.GetInteriorColors(), "InteriorColorId", "InteriorColorName");
                updatedVehicle.IsUsed = new List<SelectListItem>
                {
                    new SelectListItem {Text = "New", Value = false.ToString()},
                    new SelectListItem {Text = "Used", Value = true.ToString()}
                };
                return View(updatedVehicle);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteVehicle(EditVehicleViewModel updatedVehicle)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            var deletePath = Server.MapPath("~/Images/" + updatedVehicle.Vehicle.ImageFileName);

            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }

            repo.Delete(updatedVehicle.Vehicle.VehicleId);

            return RedirectToAction("Vehicles");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Users()
        {
            var model = new UsersViewModel();
            var usersContext = new ApplicationDbContext();
            model.Users = usersContext.Users.ToList();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddUser()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var model = new AddUserViewModel();
            model.User = new ApplicationUser();
            model.NewPassword = new SetPasswordViewModel().NewPassword;
            model.ConfirmPassword = new SetPasswordViewModel().ConfirmPassword;
            List<IdentityRole> allRoles = roleManager.Roles.ToList();

            model.Roles = new SelectList(allRoles, "Name", "Name");

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var user = new ApplicationUser();
                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                user.UserName = model.User.Email;
                user.Role = model.User.Role;
                user.Email = model.User.Email;

                var role = roleManager.Roles.Where(r => r.Id == model.User.Role).Select(r => r.Name).ToString();

                var chkUser = userManager.Create(user, model.NewPassword);

                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, model.User.Role);
                }

                return RedirectToAction("Users");
            }
            else
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                model = new AddUserViewModel();
                model.User = new ApplicationUser();
                model.NewPassword = new SetPasswordViewModel().NewPassword;
                model.ConfirmPassword = new SetPasswordViewModel().ConfirmPassword;
                List<IdentityRole> allRoles = roleManager.Roles.ToList();

                model.Roles = new SelectList(allRoles, "Name", "Name");
                return View(model);
            }
        }


        [Authorize]
        [HttpGet]
        public ActionResult EditUser(string userId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var model = new EditUserViewModel();
            var appUser = userManager.FindById(userId);
            model.UserId = appUser.Id;
            model.User = appUser;
            model.NewPassword = new SetPasswordViewModel().NewPassword;
            model.ConfirmPassword = new SetPasswordViewModel().ConfirmPassword;
            List<IdentityRole> allRoles = roleManager.Roles.ToList();
            model.Roles = new SelectList(allRoles, "Name", "Name");

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            if (ModelState.IsValid)
            {
                var user = userManager.FindById(model.UserId);
         
                var role = roleManager.FindByName(user.Role);
               
                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                user.Email = model.User.Email;
                user.Role = model.User.Role;
                user.UserName = model.User.Email;

                if (role.Name != model.User.Role)
                {
                    userManager.RemoveFromRole(model.UserId, role.Name);
                    userManager.AddToRole(model.UserId, model.User.Role);
                }
                
                if (!String.IsNullOrEmpty(model.NewPassword) && !String.IsNullOrEmpty(model.ConfirmPassword))
                {
                    userManager.RemovePassword(model.UserId);
                    userManager.AddPassword(model.UserId, model.NewPassword);
                }

                userManager.Update(model.User);

                return RedirectToAction("Users");
            }
            else
            {
                ApplicationUser appUser = new ApplicationUser();
               
                model = new EditUserViewModel();
                appUser = userManager.FindById(model.User.Id);

                model.User = appUser;
                model.NewPassword = new SetPasswordViewModel().NewPassword;
                model.ConfirmPassword = new SetPasswordViewModel().ConfirmPassword;
                List<IdentityRole> allRoles = roleManager.Roles.ToList();
                model.Roles = new SelectList(allRoles, "Name", "Name");
                
                return View(model);
            }
        }

        //private methods
        private List<SelectListItem> GetListPrices()
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var searchParams = new SalesVehicleSearchParameters();
            var vehicles = repo.SalesVehicleSearch(searchParams).OrderByDescending(m => m.MSRP);

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

        private List<SelectListItem> GetYears()
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var searchParams = new SalesVehicleSearchParameters();

            var vehicles = repo.SalesVehicleSearch(searchParams).OrderBy(y => y.Year);

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

        private int GetSpecialId(SpecialsViewModel newSpecial)
        {
            var specialsRepo = SpecialsRepositoryFactory.GetRepository();

            var specialId = specialsRepo.GetAll().Select(p => p.SpecialId).LastOrDefault() + 1;

            return specialId;
        }
    }
}