using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace T2NSHOP.Models
{
    public class SeedRolesAndUsers
    {
        public static void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Customer"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Customer"));
            }
            if (!roleManager.RoleExists("Admin"))
            {
                var roleresult = roleManager.Create(new IdentityRole("Admin"));
            }
#pragma warning disable CS0219 // The variable 'userName' is assigned but its value is never used
            string userName = "Admin";
#pragma warning restore CS0219 // The variable 'userName' is assigned but its value is never used
            string email = "Admin@gmail.com";
            string password = "Abc@123";
            ApplicationUser user = userManager.FindByName(email);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    CreateDate = DateTime.Now
                };
                userManager.Create(user, password);
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}