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
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context) { DisposeContext = false });
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

        public bool RegisterUser(ApplicationUser applicationUser, string password, ICollection<string> errors)
        {
            var result = userManager.Create(applicationUser, password);
            bool succeeded = result.Succeeded;

            if (succeeded)
            {
                userManager.AddToRole(applicationUser.Id, UserRole);
            }
            else
            {
                foreach (string error in result.Errors)
                {
                    errors.Add(error);
                }
            }

            return succeeded;
        }

        public bool ValidateCredentials(string email, string login, ICollection<string> errors)
        {
            bool validated = true;

            if (userManager.FindByEmail(email) != null)
            {
                validated = false;
                errors.Add("Such email already exists");
            }
            else if (userManager.FindByName(login) != null)
            {
                validated = false;
                errors.Add("Such login already exists");
            }

            return validated;
        }
    }
}
