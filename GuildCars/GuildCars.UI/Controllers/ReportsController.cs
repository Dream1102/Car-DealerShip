using GuildCars.Data.RepositoryFactories;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inventory()
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            var model = repo.GetInventoryReport();

            return View(model);
        }

        public ActionResult Sales()
        {
            var model = new SalesReportViewModel();
            model.UserList = GetUserList();
            return View(model);
        }

        private List<SelectListItem> GetUserList()
        {
            var model = new UsersViewModel();
            var usersContext = new ApplicationDbContext();
            var users = usersContext.Users.ToList();

            List<SelectListItem> items = users.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.FirstName + " " + p.LastName,
                    Value = p.Email
                };

            });

            return items;
        }
    }
}