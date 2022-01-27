using GuildCars.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuildCars.UI.Startup))]
namespace GuildCars.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Sales"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Sales";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Disabled"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Disabled";
                roleManager.Create(role);
            }

            var user = new ApplicationUser();
            user.FirstName = "Admin";
            user.LastName = "Seward";
            user.UserName = "admin@guildcars.com";
            user.Role = "Admin";
            user.Email = "admin@guildcars.com";


            string userPWD = "GuildyL0cks";

            var salesUser = new ApplicationUser();
            salesUser.FirstName = "Joe";
            salesUser.LastName = "Sellerson";
            salesUser.UserName = "sales@guildcars.com";
            salesUser.Role = "Sales";
            salesUser.Email = "sales@guildcars.com";

            string salesPWD = "Salesy1";

            var chkUser = userManager.Create(user, userPWD);

            if (chkUser.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, "Admin");
            }

            var chkSalesUser = userManager.Create(salesUser, salesPWD);

            if (chkSalesUser.Succeeded)
            {
                var result2 = userManager.AddToRole(salesUser.Id, "Sales");
            }
        }
    }
}
