using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace NLECA_Core_Newsletter_App.Data.Initializer
{
    public class RoleAndAdminInitializer
    {
        private readonly IConfiguration Configuration;
        private readonly string SuperAdminUserName;
        private readonly string SuperAdminPassword;
        private readonly string ReadOnlyUserName;
        private readonly string ReadOnlyPassword;

        public RoleAndAdminInitializer(IConfiguration config)
        {
            Configuration = config;
            SuperAdminUserName = Configuration["SuperAdminUser:UserName"];
            SuperAdminPassword = Configuration["SuperAdminUser:Password"];
            ReadOnlyUserName = Configuration["ReadOnlyUser:UserName"];
            ReadOnlyPassword = Configuration["ReadOnlyUser:Password"];
        }

        public void SeedData(UserManager<ApplicationIdentityUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            if (!string.IsNullOrEmpty(SuperAdminUserName) && !string.IsNullOrEmpty(SuperAdminPassword)
                && !string.IsNullOrEmpty(ReadOnlyUserName) && !string.IsNullOrEmpty(ReadOnlyPassword))
            {
                SeedRoles(roleManager);
                SeedAdmin(userManager);
            }
        }

        public void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            foreach (RoleType roleType in (RoleType[])Enum.GetValues(typeof(RoleType)))
            {
                if (!roleManager.RoleExistsAsync(roleType.ToString()).Result)
                {
                    ApplicationRole role = new ApplicationRole();
                    role.Name = roleType.ToString();
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }
            }
        }

        public void SeedAdmin(UserManager<ApplicationIdentityUser> userManager)
        {
            // ADD SUPERUSER
            if (userManager.FindByEmailAsync(SuperAdminUserName).Result == null)
            {
                ApplicationIdentityUser user = new ApplicationIdentityUser();
                user.UserName = SuperAdminUserName;
                user.Email = SuperAdminUserName;
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, SuperAdminPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RoleType.SuperAdmin.ToString()).Wait();
                }
            }

            // ADD READONLYUSER (has access to all non personal info but can't edit anything)
            if (userManager.FindByEmailAsync(ReadOnlyUserName).Result == null)
            {
                ApplicationIdentityUser user = new ApplicationIdentityUser();
                user.UserName = ReadOnlyUserName;
                user.Email = ReadOnlyUserName;
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, ReadOnlyPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RoleType.ReadOnlyUser.ToString()).Wait();
                }
            }
        }
    }
}
