using SimpleForum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Configurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            // Primary key
            this.HasKey(user => user.Id);

            // Properties
            this.Property(user => user.UserName).IsRequired().HasMaxLength(25);
            this.Property(user => user.Email).IsRequired().HasMaxLength(25);

            // Foreign keys
            this.HasOptional<UserEntity>(user => user.User)
                .WithRequired(user => user.ApplicationUser);
        }
    }
}
