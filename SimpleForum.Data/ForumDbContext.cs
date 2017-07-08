using Microsoft.AspNet.Identity.EntityFramework;
using SimpleForum.Data.Configurations;
using SimpleForum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data
{
    public class ForumDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserEntity> ForumUsers { get; set; }

        public ForumDbContext()
            : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ForumDbContext>(new DropCreateDatabaseIfModelChanges<ForumDbContext>());

            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
