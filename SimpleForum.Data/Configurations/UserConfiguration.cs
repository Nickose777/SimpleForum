using SimpleForum.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Data.Configurations
{
    class UserConfiguration : EntityTypeConfiguration<UserEntity>
    {
        public UserConfiguration()
        {
            // Primary key
            this.HasKey(user => user.Id);

            // Properties
            this.Property(user => user.FirstName).IsRequired().HasMaxLength(25);
            this.Property(user => user.LastName).IsRequired().HasMaxLength(25);
        }
    }
}
