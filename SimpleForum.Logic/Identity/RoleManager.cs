using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleForum.Core;
using SimpleForum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Identity
{
    class RoleManager : RoleManager<ApplicationRole, string>
    {
        public RoleManager(ForumDbContext context)
            : base(new RoleStore<ApplicationRole>(context)) { }
    }
}
