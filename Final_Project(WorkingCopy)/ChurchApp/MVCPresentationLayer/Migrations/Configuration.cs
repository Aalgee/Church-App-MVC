namespace MVCPresentationLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCPresentationLayer.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCPresentationLayer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVCPresentationLayer.Models.ApplicationDbContext";
        }

        protected override void Seed(MVCPresentationLayer.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@company.com";
            const string adminPassword = "P@ssw0rd";

            // new code for step 6
            // Note: There is purposely no using statement for LogicLayer. This is to force
            // programmers to use th fully qualified name for our internal UserManager, to
            // keep it clear that this is not the Identity systems UserManager class.
            LogicLayer.UserManager userMgr = new LogicLayer.UserManager();
            var roles = userMgr.RetrievePersonRoles();
            foreach(var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = role });
            }
            if (!roles.Contains("Administrator"))
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Administrator " });
                // Note: even though administrator should be in the list of roles, this check is
                // to remove any risk of it being missing due to deletion from the internal database.
            }

            if(!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    GivenName = "Admin",
                    FamilyName = "Company"
                };

                IdentityResult result = userManager.Create(user, adminPassword);
                context.SaveChanges(); // updates the database

                // this code will add the Administrator role to admin
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                    context.SaveChanges();
                }
            }
        }
    }
}
