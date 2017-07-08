using SimpleForum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Contracts.Repositories
{
    public interface IAccountManager
    {
        void InitializeRoles(IEnumerable<string> roles);

        bool RegisterUser(ApplicationUser appUser, string password, ref string errorMessage);

        bool ValidateCredentials(string email, string login, ref string errorMessage);
    }
}
