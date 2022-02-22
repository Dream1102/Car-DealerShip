using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.Specials = SpecialsRepositoryFactory.GetRepository().GetAllSpecials();
            model.Vehicles = VehicleRepositoryFactory.GetRepository().GetFeaturedVehicles();

            return View(model);
        }

        [HttpGet]
        public ActionResult Specials()
        {
            var model = SpecialsRepositoryFactory.GetRepository().GetAllSpecials();

            return View(model);
        }

        [HttpGet]
        public ActionResult Contact()
        { 
            var model = new ContactRequestAddViewModel();
            var url = Request.RawUrl;

            if(url.Length > 14)
            {
                model.ContactMessage = url.Substring(14);
                return View(model);
            }
            else
            {
                return View(model);
            } 
        }

        [HttpPost]
        public ActionResult Contact(ContactRequestAddViewModel contactRequest)
        {
            if (ModelState.IsValid)
            {
                var repo = ContactRequestRepositoryFactory.GetRepository();
                var allContactRequests = repo.GetAllContactRequests();
                var url = Request.RawUrl;
                contactRequest.ContactRequestId = allContactRequests.Select(c => c.ContactRequestId).LastOrDefault() + 1;

                if(url.Length > 14)
                {
                    contactRequest.VehicleId = url.Substring(14);
                }

                ContactRequest requestToAdd = new ContactRequest();
                requestToAdd.ContactName = contactRequest.ContactName;
                requestToAdd.VehicleId = contactRequest.VehicleId;
                requestToAdd.ContactEmail = contactRequest.ContactEmail;
                requestToAdd.ContactPhone = contactRequest.ContactPhone;
                requestToAdd.ContactRequestId = contactRequest.ContactRequestId;
                requestToAdd.ContactMessage = contactRequest.ContactMessage;
                requestToAdd.DateContactRequestCreated = DateTime.Now;

                repo.InsertContactRequest(requestToAdd);
                return RedirectToAction("Index");
            }
            else
            {
                return View(contactRequest);
            }
        }
    }
}