using Microsoft.Owin.Security;
using Ninject.Modules;
using Ninject.Web.Common;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.Services;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleForum.NinjectModules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUserService>().To<UserService>();
            this.Bind<IAccountService>().To<AccountService>();
            this.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication)
               .InRequestScope();
        }
    }
}