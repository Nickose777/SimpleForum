using SimpleForum.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Core.Configurations
{
    class MessageConfiguration : EntityTypeConfiguration<MessageEntity>
    {
        public MessageConfiguration()
        {
            //Primary key
            this.HasKey(message => message.Id);

            //Properties
            this.Property(message => message.Text)
                .IsRequired()
                .HasMaxLength(200);

            //Foreign keys
            this.HasRequired(message => message.Sender)
                .WithMany(sender => sender.Messages)
                .HasForeignKey(message => message.SenderId);
            this.HasRequired(message => message.Topic)
                .WithMany(topic => topic.Messages)
                .HasForeignKey(message => message.TopicId);
        }
    }
}
