using Ninject.Modules;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.Services;
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
        }
    }
}