using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleForum.Data.Contracts.Repositories;
using SimpleForum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Identity
{
    class AccountManager : IAccountManager
    {
        public const string UserRole = "User";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AccountManager(ForumDbContext context)
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context) { DisposeContext = false, AutoSaveChanges = false });
            roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context) { DisposeContext = false });
        }

        public void InitializeRoles(IEnumerable<string> roles)
        {
            foreach (string role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new ApplicationRole { Name = role });
                }
            }
        }

        public bool RegisterUser(ApplicationUser applicationUser)
        {
            bool succeeded = userManager.Create(applicationUser).Succeeded;

            if (succeeded)
            {
                userManager.AddToRole(applicationUser.Id, UserRole);
            }

            return succeeded;
        }
    }
}
