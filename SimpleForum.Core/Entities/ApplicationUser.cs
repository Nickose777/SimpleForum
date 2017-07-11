using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleForum.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual UserEntity User { get; set; }
    }
}
