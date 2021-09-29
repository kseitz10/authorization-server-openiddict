using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthorizationServer.Application.Common.Security;
using AuthorizationServer.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace AuthorizationServer.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await CreateRoleIfNotExists(roleManager, UserRoles.ApplicationAdministrator);
            await CreateRoleIfNotExists(roleManager, UserRoles.UserAdministrator);

            var administrator = new ApplicationUser { UserName = "admin@localhost", Email = "admin@localhost" };
            await CreateUserIfNotExists(userManager, administrator, "Admin1!", new[] { UserRoles.ApplicationAdministrator, UserRoles.UserAdministrator });

            var standardUser = new ApplicationUser { UserName = "user@localhost", Email = "user@localhost" };
            await CreateUserIfNotExists(userManager, standardUser, "User1!", Array.Empty<string>());
        }

        private static async Task CreateUserIfNotExists(UserManager<ApplicationUser> userManager, ApplicationUser user, string password, string[] roles)
        {
            if (userManager.Users.All(u => u.UserName != user.UserName))
            {
                await userManager.CreateAsync(user, password);
                await userManager.AddToRolesAsync(user, roles);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await userManager.ConfirmEmailAsync(user, token);
            }
        }

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = new IdentityRole(roleName);
            if (roleManager.Roles.All(r => r.Name != role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}