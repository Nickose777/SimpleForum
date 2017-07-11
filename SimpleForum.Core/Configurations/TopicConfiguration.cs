using SimpleForum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Core.Configurations
{
    class TopicConfiguration : EntityTypeConfiguration<TopicEntity>
    {
        public TopicConfiguration()
        {
            //Primary key
            this.HasKey(topic => topic.Id);

            //Properties
            this.Property(topic => topic.Title)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(topic => topic.Description)
                .IsRequired()
                .HasMaxLength(200);

            //Foreign keys
            this.HasRequired(topic => topic.Creator)
                .WithMany(sender => sender.Topics)
                .HasForeignKey(topic => topic.CreatorId)
                .WillCascadeOnDelete(false);
        }
    }
}
