using Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class IdentitySeed
    {
        protected IdentitySeed() { }
        public static async Task SeedAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {

            if (dbContext.Database.IsSqlServer())
            {
                dbContext.Database.Migrate();
            }

            string adminUserName = "admin@gmail.com";
            string defaultUserName = "user@gmail.com";

            await roleManager.CreateAsync(new ApplicationRole(Constants.ROLE_ADMINISTRATORS));
            await roleManager.CreateAsync(new ApplicationRole(Constants.ROLE_USER));

            var defaultUser = new ApplicationUser { UserName = defaultUserName, Email = defaultUserName };
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };

            await userManager.CreateAsync(defaultUser, Constants.DEFAULT_PASSWORD);
            await userManager.CreateAsync(adminUser, Constants.DEFAULT_PASSWORD);


            adminUser = await userManager.FindByNameAsync(adminUserName);
            defaultUser = await userManager.FindByNameAsync(defaultUserName);

            await userManager.AddToRoleAsync(adminUser, Constants.ROLE_ADMINISTRATORS);
            await userManager.AddToRoleAsync(defaultUser, Constants.ROLE_USER);

        }
    }
}
