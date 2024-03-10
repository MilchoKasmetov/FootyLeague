namespace FootyLeague.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FootyLeague.Common;
    using FootyLeague.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser
            {
                UserName = GlobalConstants.AdministratorUserName,
                Email = GlobalConstants.AdministratorEmail,
            };

            var result = await userManager.CreateAsync(user, GlobalConstants.AdministratorPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
