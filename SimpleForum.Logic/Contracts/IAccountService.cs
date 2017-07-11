using Microsoft.Owin.Security;
using SimpleForum.Logic.DTO.User;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Contracts
{
    public interface IAccountService : IDisposable
    {
        ServiceMessage RegisterUser(UserRegisterDTO user);

        ServiceMessage SignIn(string login, string password);

        void SignOut();
    }
}
