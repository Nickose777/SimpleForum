using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SimpleForum.Core;
using SimpleForum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Identity
{
    class SignInManager : SignInManager<ApplicationUser, string>
    {
        public SignInManager(UserManager<ApplicationUser, string> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) { }
    }
}
